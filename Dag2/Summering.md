# Dag 2

## Delegater
Delegater tillåter oss att ha metoder som parametrar. 
En delegat deklareras enligt
	
	delegate void DelgateName(parameters);
	
Tex
	
	delegate void Print(string s);
	
Nu kan alla metoder som matchar signaturen ovan användas som parametrar till delagaten

Tex

	// Via anonym funktion (Action)
	Print p = new Print((s) => { Console.WriteLine("PrintAction :"+s);}); 

	// via inline delegate
	Print p = new Print(delegate (string s) { Console.WriteLine("DelegateAction: " + s); });

	// Via metod instans
	Print p = new Print(myPrint);
	public static void myPrint(string s)
	{
		 Console.WriteLine("myPrint :"+s);}
	}

Själva anropet utförs genom att anropa delegaten

	p("Hello all delegate funtions");

Ovan satte vi en delegate funktion i taget, men man kan använda multicast delegater genom +=

	Print printDelegate = new Print(myPrint);
    printDelegate += (s) => { Console.WriteLine("Action: " + s); };
    printDelegate += delegate (string s) { Console.WriteLine("DelegateAction: " + s); };
    
    // kör alla delegater med multicast
    printDelegate("hello");

	//Man kan ta bort delegater med -=

## Events
Event är i grunden delegater, men det är nu enklare att använda genom EventHandler och EventHandler<T>

	// Custom EventArgs som ger info om tex en bild som behandlats
	public class ImageCompletedEventArgs : EventArgs
    {
        public string ImageName { get; set; }
        public int ImageSize { get; set; }
    }
	// Bildtjänsten vet inte vilka som är intresserade av att veta när en bild är klar, använder därför event så att andra tjänster kan bestämma själva om de vill ha info och vad som ska göras.
    public class ImageService
    {
		// Publikt event med vårat custom EventArgs
        public event EventHandler<ImageCompletedEventArgs> OnImageCompletedEvent;
		//Vi fejkar en lång process
        public void ProcessImage()
        {
            //fake something...
            Thread.Sleep(5000);
            //Notify subscribers med vårt custom EventArgs
            OnImageCompleted(new ImageCompletedEventArgs { ImageName ="fake.jpg",ImageSize = 100 });  
        }

        public void OnImageCompleted(ImageCompletedEventArgs e)
        {
			//Om någon subscriber finns anropa
            if (OnImageCompletedEvent != null)
            {
                OnImageCompletedEvent(this, e);
            }
        }
    }

	//Vår Sms-tjänst vill veta när en bild är klar...
    public class SmsService
    {
        public void OnImageCompleted(object sender, ImageCompletedEventArgs e)
        {
            Console.WriteLine("SmsService Notified About ImageCompleted Name {0}, Size {1}", e.ImageName, e.ImageSize);
        }
    }

	// I Main...
	var imageService = new ImageService();

    var smsService = new SmsService();
	// När en bild är klar ska vår SmsService få reda på detta
    imageService.OnImageCompletedEvent += smsService.OnImageCompleted;           

	// Processa en bild
    imageService.ProcessImage();
    Console.ReadLine();

## Tasks, Async/Await 

### Starta Tasks

	
	await Task.Run(()=> { Console.WriteLine("Task started with Task.Run"); });

    await Task.Factory.StartNew(() => { Console.WriteLine("Task started with Task.Factory.StartNew"); });

    new Task(()=> { Console.WriteLine("Task started with Task.Run"); }).Start();

### Avbryta Tasks med cancellationToken

	var cts = new CancellationTokenSource(5000);
    //Start a new task and wait for it. Will be cancelled after 5 sec
    await Task.Run(async () =>
    {
        try
        {
            while (true)
            {
                Console.WriteLine(DateTime.Now);
				// Väl inne i vår task måste vi använda vår token för att kolla om vi ska avbryta.
				// Har man ingen TaskDelay som tar en token så får man kolla på IsCancellationRequested
				//if (cts.IsCancellationRequested)
                //    break;
                await Task.Delay(1000, cts.Token);
            }
        }
        catch (TaskCanceledException tce) { Console.WriteLine("Task Cancelled"); }
        catch (Exception ex)
        {
            Console.WriteLine("EX " + ex.Message);
        }
    }, cts.Token);

### WaitAny

	Console.WriteLine("***WaitAnyExample***");
    var t1 = Task.Run(async () => { await Task.Delay(1000); Console.WriteLine("t1 done"); });
    var t2 = Task.Run(async () => { await Task.Delay(2000); Console.WriteLine("t2 done"); });
    var t3 = Task.Run(async () => { await Task.Delay(3000); Console.WriteLine("t3 done"); });

    Console.WriteLine("Waiting any");
	// Kommer bara att vänta på task 1 eftersom den är klar först
    Task.WaitAny(t1, t2, t3);
    Console.WriteLine("Waiting any done");

### WaitAll

Notera skillnaden i exekveringstid mellan att vänta individuellt och att vänta på alla samtidigt!!!

	Console.WriteLine("***WaitAllExample***");
    Console.WriteLine("Waiting all");
    var sw = Stopwatch.StartNew();

    var t1 = Task.Run(async () => { await Task.Delay(1000); Console.WriteLine("t1 done"); });
    var t2 = Task.Run(async () => { await Task.Delay(2000); Console.WriteLine("t2 done"); });
    var t3 = Task.Run(async () => { await Task.Delay(3000); Console.WriteLine("t3 done"); });


    Task.WaitAll(t1, t2, t3);
    sw.Stop();
    Console.WriteLine("Waiting all done in {0}", sw.Elapsed.TotalMilliseconds);

    Console.WriteLine("Waiting individually");
    sw.Restart();
    await Task.Run(async () => { await Task.Delay(1000); Console.WriteLine("t1 done"); });
    await Task.Run(async () => { await Task.Delay(2000); Console.WriteLine("t2 done"); });
    await Task.Run(async () => { await Task.Delay(3000); Console.WriteLine("t3 done"); });
            
    sw.Stop();
    Console.WriteLine("Waiting individually done in {0}", sw.Elapsed.TotalMilliseconds);

## Parallel

Parallell.Invoke låter oss köra actions parallelt. Vi kan sätta MaxDegreeOfParallelism.
Om vi sätter -1 så är det obegränsad parallelism, 1 är sekventiellt, men man kan sätta andra värden också

	public static void ParallelExample(int parallel)
    {
        Console.WriteLine("***ParallelExample With Parallel "+parallel+"***");
        var myTasks = new List<Action>(Environment.ProcessorCount);
        for(var i = 0; i < Environment.ProcessorCount; i++)
        {
            var s = "Task " + i;
            myTasks.Add(() => 
            {
                var sw = Stopwatch.StartNew();

                var r = 0;
                for (var x = 0; x < 1000000; x++)
                {
                    r += x;
                }
                sw.Stop();
                Console.WriteLine("Task " + s + " with parallell " + parallel + " took " + sw.Elapsed.TotalMilliseconds);
            });
        }
        var swTot = Stopwatch.StartNew();
        var options = new ParallelOptions { MaxDegreeOfParallelism = parallel };
        Parallel.Invoke(myTasks.ToArray());
        Console.WriteLine("All tasks with parallell " + parallel + " took " + swTot.Elapsed.TotalMilliseconds);
    }