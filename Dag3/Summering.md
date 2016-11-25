# Dag 3

## Interfaces

Ett interface är som ett kontrakt som anger vad som ska implementeras.
Interfacet ICar can tex ange att det ska finnas metoder för StartEngine, StopEngine samt att det ska finnas metoder för CheckFuel etc.

ICar kan då se ut som nedan

	public interface ICar
	{
		void StartEngine();
		void StopEngine();

		double CheckFuel();

		//etc...
	}

Så ett interface ska inte (och kan inte) ange hur något fungerar, utan bara vad som ska implementeras.

En klass kan ha implementera multiple interface vilket gör interface mycket mer flexibla än tex abtrakta basklasser.
Dessutom gör interface att man kan få löst kopplad arkitektur och byta ut implementationer så länge den nya implementationen implementerar samma interface som den gamla implementationen.

Kika gärna på DI/IoC (Dependency Injection / Inversion Of Control) ramverk så ser ni styrkan med att implementera interface.

	//Exempel på implementation av ICar
	public class Volvo : ICar
	{
		public void StartEngine()
		{
			// logic here	
		}

		public void StopEngine()
		{
			// logic here
		}

		public double CheckFuel()
		{
			// logic here
		}
	}

Ett bra exempel på användandet av interface är RepositoryPattern, även om vi i exemplet med repository pattern i denna kurs inte använder oss av något DI/IoC ramverk.

Om vi haft ett ramverk för DI/IoC hade vi först registerat vilken konkret typ som ett interface ska returnera.

	//Påhittad syntax...
	Resolver.Map<ICar>.To<Volvo>();

	//För att sedan få tag på ICar genom
	var car = Resolver.Get<ICar>();

På detta sätt kan vi byta ut Volvo mot en annan implementation av ICar utan att behöva skriva om användandet av ICar i vår applikation.

## Repository Pattern med EF Code First

För att köra denna lab behöver vi SQL Express with advanced services.
Download: https://www.microsoft.com/en-US/download/details.aspx?id=42299
Välj: ExpressAdv 64BIT\SQLEXPRADV_x64_ENU.exe


### Core
Katalogen Core i exemplet motsvarar ett klassbibliotek, men ligger direkt i exemplet som en katalog för att förenkla.
Core innehåller vår domänmodell samt de interface som används i vårt RepositoryPattern.
Ni kan se att genom att använda generiska interface sparar vi mycket kod i de repository interface vi skriver för våra entiteter (i detta fall endast IAnimalRepository).

Enligt Martin Fowler ska ett repository i RepositoryPattern fungera som en in-memory collection.
Det finns ingen Save/Execute metod, utan endast Add, Remove, Update, Find, Get, GetAll osv.
Helt enkelt generell (generisk) funktionalitet gemensamt för alla entiteter.

Behöver man specifik funktionalitet för en entitet ska detta specificeras i tex IAnimalRepository för att sedan implementeras i den konkreta type AnimalRepository.

UnitOfWork innehåller funktionalitet för att utföra operationer mot vårt repository. I detta fall endast Complete() vilket ser till att utföra Create, Update, Delete i databasen.

### Persistence

Katalogen Persistence i exemplet motsvarar ett klassbibliotek, men ligger direkt i exemplet som en katalog för att förenkla.
Persistence innehåller de konkreta implementationerna av de interface som fanns i Core.

Precis som i Core sparar vi mycket kod på generiska klasserna och interface. Tex så har AnimalRepository endast en metod för vårt specifika krav på GetMostDangerousAnimals

### Entity Framework Code First

Code First gör det enkelt att utveckla med DDD (Domain Driven Design) och fungerar mycket bra med RepositoryPattern.

#### Install-Package
 
Om ni skulle får kompileringsfel när ni bygger detta projekt så ta bort EntityFramework från referenserna samt från package.config.
Kör sedan...

	Install-Package EntityFramework

#### Enable-Migrations

Första gången man ska skapa sin databas kör man Enable-Migrations. EF kommer då att lägga till katalogen Migrations och en configuration.
Detta är redan gjort så ni behöver inte göra detta.
	
	Enable-Migrations

Man ska också skapa en migrering första gången (och varje gång man ändrat sin domain model).
Migreringar ger oss möjlighet att uppdatera och backa tillbaka funktionalitet. Kika i migrations katalogen så ser ni att det finns Up() och Down() metoder för migrering.

#### Add-Migration

	Add-Migration InitialMigration

När man kör migreringen så ger man den ett namn som säger vad uppdateringen innehåller. tex AddedNewProperty

#### Update-Database

När man vill uppdatera sin databas kör man Update-Database. Detta kommer att trigga Seed metoden i Configuration klassen (kika under Migrations katalogen).
I denna demo körs koden nedan. 

	context.Animals.AddOrUpdate(p => p.Name,
    new Core.DomainModel.Animal { Name = "Tiger", Age = 23, DangerScale = 10, Dangerous = true },
    new Core.DomainModel.Animal { Name = "Elephant", Age = 90, DangerScale = 5, Dangerous = false },
    new Core.DomainModel.Animal { Name = "Mouse", Age = 1, DangerScale = 0, Dangerous = false},
    new Core.DomainModel.Animal { Name = "Badger", Age = 12, DangerScale = 4, Dangerous = false},
    new Core.DomainModel.Animal { Name = "Hippo", Age = 56, DangerScale = 9, Dangerous = true },
    new Core.DomainModel.Animal { Name = "Black Mamba", Age = 11, DangerScale = 10, Dangerous = true },
    new Core.DomainModel.Animal { Name = "Mosquito", Age = 3, DangerScale = 10, Dangerous = true },
    new Core.DomainModel.Animal { Name = "Swan", Age = 8, DangerScale = 3, Dangerous = false },
    new Core.DomainModel.Animal { Name = "Horse", Age = 4, DangerScale = 3, Dangerous = false},
    new Core.DomainModel.Animal { Name = "Shark", Age = 35, DangerScale = 8, Dangerous = true });

Vi säger att vi endast kör AddOrUpdate på entiteter där Name inte redan finns i databasen. 

	
	Update-Database

Detta steg är det enda steget ni beöver köra om det är första gången ni öppnar denna solution.
Om ni har en SqlExpress installerad så kommer Seed metoden att köras och ni kommer att få ett gäng rader i databasen (som dessutom skapas åt er).

## Blocking Collection (Producer/Consumer Pattern)

BlockingColleciton är en enkel generisk collection som ger oss ett producer/consumer pattern.

Notera att genom att endast tillåta en capacity på 10 så kommer Add att blockas när det finns 10 items i vår collection.
När sedan en consumer tar ett item så kan man lägga till igen.
Väljer man att inte sätta en capacity så kommer man att kunna lägga till hur mycket man vill


	// Tillåter endast 10 items av typen int. Den kommer att blocka om vi försöker lägga till och vår collection är full.
	static BlockingCollection&lt;int&gt; blockingCollection = new BlockingCollection&lt;int&gt;(10);

    //Task som lägger till 2 tal i sekunden
    Task.Run(async () =>
    {
        for (var i = 0; i < 100; i++)
        {
            blockingCollection.Add(i);
            Console.WriteLine("Produced: " + i);
            await Task.Delay(500);
        }
    });

    //En task som endast konsumerar ett värde från vår collection va 15:e sekund, vilket kommer att blocka addering av items då vi har satt capacity till 10.
    //Normalt sett så vill man skriva och läsa (produce/consume) så snabbt som möjligt, det vi gör här är bara för att förstå hur BlockingCollection&lt;T&gt; fungerar. 
    Task.Run(async () => {
        Console.WriteLine("Starting consumer");
        // Viktigt: När det inte finns några items att läsa kommer GetConsumingEnumerable() att blocka (vänta) på att ett item dyker upp.
        // Detta ska därför göras i en Task så att man inte blockerar ALLT!
        foreach (var num in blockingCollection.GetConsumingEnumerable())
        {
            await Task.Delay(15000);
            Console.WriteLine("Consumed: " + num);
        }
        // Hit kommer vi inte då blockingCollection alltid blockar på GetConsumingEnumerable()
        Console.WriteLine("Ending consumer");
    });
            

## Custom Attribute

Vi kan genom att ärva Attribute skapa egna attribut för att dekorera all från klasser, metoder, properties, fields, parametrar, constructor etc.

	// Exempel på ett attribut som låter oss skriva en kommentar till alla typer tack vare AttributeTargets.All
    [AttributeUsage(AttributeTargets.All)]
    public class Comment : Attribute
    {
        public string Value { get; set; }
        public Comment(string v)
        {
            this.Value = v;
        }
    }

	// Användning av vårt custom attribut
    [Comment("This is our cat...")]
    public class Cat
    {
        public int Age { get; set; }
        public string Name { get; set; }

        public string Says()
        {
            return "Mjau";
        }
    }

Vi kan med reflection hitta igen vårt attribut och visa information om användningen av det, vilket vi gör i nästa avsnitt.

## Reflection

Med reflection kan vi ta reda på information om alla Assemblies och dess innehåll som finns laddat i vår AppDomain.
Vi kan så klart ladda in nya assemblies om vi vill, men vi nöjer oss här med att undersöka typen Cat i runtime.

För att hitta information med reflection behöver vi inte någon instans av den typ vi ska undersöka.
Vill vi däremot anropa metoder, köra get/set på properties etc så måste vi ha en instans av typen.

### Visa och Ändra objekt med reflection 
	// Get info - no  instance needed
    var animal = typeof(Cat);
	// List all properties.. Name and type
    foreach (var p in animal.GetProperties())
    {
        Console.WriteLine(p.Name + ", " + p.PropertyType.Name);
    }

    // set info with reflection
    var c = new Cat();
    c.Age = 23;
	// Get property info for Age
    PropertyInfo prop = typeof(Cat).GetProperty("Age");//, BindingFlags.Public | BindingFlags.Instance);
    if (null != prop && prop.CanWrite)
    {
		// set age to 45 on the instance named 'c'
        prop.SetValue(c, 45, null);
    }
	// show the updated age on 'c'
    Console.WriteLine(c.Age);

    // call method with reflection
    MethodInfo mi = typeof(Cat).GetMethod("Says");
    Console.WriteLine(mi.Invoke(c, null));

### Hitta attribut med Reflection

Genom att använda metoden GetCustomAttributes kan vi hitta igen vårt CommentAttribute
	
	var animal = typeof(Cat);
	//Find attribute with reflection
    var comment = (Comment)animal.GetCustomAttributes(typeof(Comment), false).FirstOrDefault();
    if (comment != null)
        Console.WriteLine("Comment for Cat = " + comment.Value);

### Snyggare att göra med extensionmetoder

	//Find attribute with extension method (cleaner and easier to use and re-use)
	comment = AttributeHelper.GetCustomAttribute<Cat, Comment>();
	if (comment != null)
		Console.WriteLine("Comment for Cat = " + comment.Value);


	public static class AttributeHelper
	{
		public static TAttribute GetCustomAttribute<TType, TAttribute>()
		{
			if (typeof(TType).GetCustomAttributes(typeof(TAttribute), false).Any())
			{
				return (TAttribute)typeof(TType).GetCustomAttributes(typeof(TAttribute), false).First();
			}
			return default(TAttribute);
		}
	}

## SemaphoreSlim/Semaphore/Lock

Vi kikar på 3 olika sätt hålla koll på hur trådar får accessa kod samtidigt.

### Lock

Med lock kan vi låsa access till koden inom vårt lock-statement. Lock används tillsammans med en deklarerad variable.
Lock är enkelt, men det är viktigt att förstå hur det fungerar med valet av variabel vi använder i lock.

Om vi tex använder lock på en static variable så förhindrar detta samtidig access från alla trådar, endast en tråd i AppDomain kan accessa vårt lock-statement.

	private static object locker = new object();
	lock(locker)
	{
		//lock på static, endast en tråd i taget kan vara här
	}

Om vi däremot använder en variable på instansen så låser lock för access inom den instans vi är i.
	
	private object locker = new object();
	lock(locker)
	{
		//lock på member, endast en tråd i taget för denna instans kan vara här
	}

### SemaphoreSlim

SemaphoreSlim ger oss möjlighet att tillåta x antal trådar samtidig access till ett avsnitt med kod.
Detta görs med metoderna Wait() och Release().

När man instansierar sin SemaphoreSlim kan man direkt sätta hur många som har access, samt hur många som maximalt kan ha access.

Exempel

    // Appdomain semaphore that accepts 2 concurrent threads 
    static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(2, 2);

	// Start 8 actions. All will start, but block so that only 2 (in this case) can access the semaphore area at the same time.
    var semaphoreslimTasks = new List<Task>();
    for (var i = 0; i < 8; i++)
    {
        Console.WriteLine("Starting Action " + i);
        var s = i.ToString();
        semaphoreslimTasks.Add(Task.Run(() => DoSemaphoreSlimStuff(s)));
    }

    Task.WaitAll(semaphoreslimTasks.ToArray());
    Console.WriteLine("All semaphoreslim tasks completed");

	// metod med begränsad samtidig access
	public static void DoSemaphoreSlimStuff(string actionNr)
    {
        Console.WriteLine("Action nr {0} is waiting in semaphoreslim land for threadid {1}", actionNr,Thread.CurrentThread.ManagedThreadId);
        semaphoreSlim.Wait();
        Console.WriteLine("Action nr {0} was granted access to semaphoreslim land for threadid {1}",actionNr, Thread.CurrentThread.ManagedThreadId);
        Thread.Sleep(5000);
        Console.WriteLine("Action nr {0} work done in semaphoreslim land, releasing threadid {1}", actionNr, Thread.CurrentThread.ManagedThreadId);
        semaphoreSlim.Release();
    }

### Semaphore

Till skillnad mot semaphoreSlim så kan vi låta Semaphore vara system wide genom att ge den ett namn.
På så sätt kan bara rätt antal trådar accessa koden oavsett vilka processer de körs i, alltså inte bara AppDomain i detta fall!!!

Exempel 

    // System-Wide semaphore that accepts 2 concurrent threads 
    static Semaphore semaphore = new Semaphore(1, 1,"SEMAPHOREDEMO");

    // Start 8 actions. All will start, but block so that only 2 (in this case) can access the semaphore area at the same time.
    // This semaphore is system wide so that it will block access over process... To test this start several instances of the exe and you will see that it will block between processes!
    var semaphoreTasks = new List<Task>();
    for (var i = 0; i < 8; i++)
    {
        Console.WriteLine("Starting Action " + i);
        var s = i.ToString();
        semaphoreTasks.Add(Task.Run(() => DoSemaphoreStuff(s)));
    }

    Task.WaitAll(semaphoreTasks.ToArray());
    Console.WriteLine("All semaphore tasks completed");

	// metod med begränsad samtidig access SYSTEM-WIDE
	public static void DoSemaphoreStuff(string actionNr)
    {
        Console.WriteLine("Action nr {0} is waiting in semaphore land for threadid {1}", actionNr, Thread.CurrentThread.ManagedThreadId);
        semaphore.WaitOne();
        Console.WriteLine("Action nr {0} was granted access to semaphore land for threadid {1}", actionNr, Thread.CurrentThread.ManagedThreadId);
        Thread.Sleep(5000);
        Console.WriteLine("Action nr {0} work done in semaphore land, releasing threadid {1}", actionNr, Thread.CurrentThread.ManagedThreadId);
        semaphore.Release();
    }

Tips: Kör demon Semaphore genom att starta minst 2 instanser av SemaphoreDemo.exe och se att Semaphore gäller system wide!

## PreProcessor Directives

Ni kan hitta hela listan av preprocessor directik här
https://msdn.microsoft.com/sv-se/library/ed8yd1ha.aspx

Direktivet nedan kommer endast att kompilera och visa en av Console.WriteLine raderna nedan.
Den andra kommer alltså att ignoreras av kompilatorn och inte ens finnas med.

	#if DEBUG
		Console.WriteLine("THIS CODE IS COMPILED & EXECUTED SINCE DEBUG IS DEFINED");
	#else
		Console.WriteLine("THIS CODE IS COMPILED & EXECUTED SINCE DEBUG IS NOT DEFINED");
	#endif

Som ett alternativ till #if #else etc kan man också dekorera metoder som ska ignoreras med attributet [Conditional]

### Exempel
Metoden nedan används i Semaphore exemplet. Om vi dekorerar denna med Conditional("DEBUG") så kommer kompilatorn att ignorera kompileringen av alla anrop till metoden om DEBUG inte är definerat!

Beskrivning: Indicates to compilers that a method call or attribute should be ignored unless a specified conditional compilation symbol is defined.

	[Conditional("DEBUG")]
    public static void DoSemaphoreSlimStuff(string actionNr)
    {
        Console.WriteLine("Action nr {0} is waiting in semaphoreslim land for threadid {1}", actionNr,Thread.CurrentThread.ManagedThreadId);
        semaphoreSlim.Wait();
        Console.WriteLine("Action nr {0} was granted access to semaphoreslim land for threadid {1}",actionNr, Thread.CurrentThread.ManagedThreadId);
        Thread.Sleep(5000);
        Console.WriteLine("Action nr {0} work done in semaphoreslim land, releasing threadid {1}", actionNr, Thread.CurrentThread.ManagedThreadId);
        semaphoreSlim.Release();
    }

Om ni byter till Release och kör Semaphore exemplet så kommer inte metoden ovan att vara med i kompileringen eller anropas.