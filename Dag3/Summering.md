# Dag 3

## Interfaces

Ett interface �r som ett kontrakt som anger vad som ska implementeras.
Interfacet ICar can tex ange att det ska finnas metoder f�r StartEngine, StopEngine samt att det ska finnas metoder f�r CheckFuel etc.

ICar kan d� se ut som nedan

	public interface ICar
	{
		void StartEngine();
		void StopEngine();

		double CheckFuel();

		//etc...
	}

S� ett interface ska inte (och kan inte) ange hur n�got fungerar, utan bara vad som ska implementeras.

En klass kan ha implementera multiple interface vilket g�r interface mycket mer flexibla �n tex abtrakta basklasser.
Dessutom g�r interface att man kan f� l�st kopplad arkitektur och byta ut implementationer s� l�nge den nya implementationen implementerar samma interface som den gamla implementationen.

Kika g�rna p� DI/IoC (Dependency Injection / Inversion Of Control) ramverk s� ser ni styrkan med att implementera interface.

	//Exempel p� implementation av ICar
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

Ett bra exempel p� anv�ndandet av interface �r RepositoryPattern, �ven om vi i exemplet med repository pattern i denna kurs inte anv�nder oss av n�got DI/IoC ramverk.

Om vi haft ett ramverk f�r DI/IoC hade vi f�rst registerat vilken konkret typ som ett interface ska returnera.

	//P�hittad syntax...
	Resolver.Map<ICar>.To<Volvo>();

	//F�r att sedan f� tag p� ICar genom
	var car = Resolver.Get<ICar>();

P� detta s�tt kan vi byta ut Volvo mot en annan implementation av ICar utan att beh�va skriva om anv�ndandet av ICar i v�r applikation.

## Repository Pattern med EF Code First

F�r att k�ra denna lab beh�ver vi SQL Express with advanced services.
Download: https://www.microsoft.com/en-US/download/details.aspx?id=42299
V�lj: ExpressAdv 64BIT\SQLEXPRADV_x64_ENU.exe


### Core
Katalogen Core i exemplet motsvarar ett klassbibliotek, men ligger direkt i exemplet som en katalog f�r att f�renkla.
Core inneh�ller v�r dom�nmodell samt de interface som anv�nds i v�rt RepositoryPattern.
Ni kan se att genom att anv�nda generiska interface sparar vi mycket kod i de repository interface vi skriver f�r v�ra entiteter (i detta fall endast IAnimalRepository).

Enligt Martin Fowler ska ett repository i RepositoryPattern fungera som en in-memory collection.
Det finns ingen Save/Execute metod, utan endast Add, Remove, Update, Find, Get, GetAll osv.
Helt enkelt generell (generisk) funktionalitet gemensamt f�r alla entiteter.

Beh�ver man specifik funktionalitet f�r en entitet ska detta specificeras i tex IAnimalRepository f�r att sedan implementeras i den konkreta type AnimalRepository.

UnitOfWork inneh�ller funktionalitet f�r att utf�ra operationer mot v�rt repository. I detta fall endast Complete() vilket ser till att utf�ra Create, Update, Delete i databasen.

### Persistence

Katalogen Persistence i exemplet motsvarar ett klassbibliotek, men ligger direkt i exemplet som en katalog f�r att f�renkla.
Persistence inneh�ller de konkreta implementationerna av de interface som fanns i Core.

Precis som i Core sparar vi mycket kod p� generiska klasserna och interface. Tex s� har AnimalRepository endast en metod f�r v�rt specifika krav p� GetMostDangerousAnimals

### Entity Framework Code First

Code First g�r det enkelt att utveckla med DDD (Domain Driven Design) och fungerar mycket bra med RepositoryPattern.

#### Install-Package
 
Om ni skulle f�r kompileringsfel n�r ni bygger detta projekt s� ta bort EntityFramework fr�n referenserna samt fr�n package.config.
K�r sedan...

	Install-Package EntityFramework

#### Enable-Migrations

F�rsta g�ngen man ska skapa sin databas k�r man Enable-Migrations. EF kommer d� att l�gga till katalogen Migrations och en configuration.
Detta �r redan gjort s� ni beh�ver inte g�ra detta.
	
	Enable-Migrations

Man ska ocks� skapa en migrering f�rsta g�ngen (och varje g�ng man �ndrat sin domain model).
Migreringar ger oss m�jlighet att uppdatera och backa tillbaka funktionalitet. Kika i migrations katalogen s� ser ni att det finns Up() och Down() metoder f�r migrering.

#### Add-Migration

	Add-Migration InitialMigration

N�r man k�r migreringen s� ger man den ett namn som s�ger vad uppdateringen inneh�ller. tex AddedNewProperty

#### Update-Database

N�r man vill uppdatera sin databas k�r man Update-Database. Detta kommer att trigga Seed metoden i Configuration klassen (kika under Migrations katalogen).
I denna demo k�rs koden nedan. 

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

Vi s�ger att vi endast k�r AddOrUpdate p� entiteter d�r Name inte redan finns i databasen. 

	
	Update-Database

Detta steg �r det enda steget ni be�ver k�ra om det �r f�rsta g�ngen ni �ppnar denna solution.
Om ni har en SqlExpress installerad s� kommer Seed metoden att k�ras och ni kommer att f� ett g�ng rader i databasen (som dessutom skapas �t er).

## Blocking Collection (Producer/Consumer Pattern)

BlockingColleciton �r en enkel generisk collection som ger oss ett producer/consumer pattern.

Notera att genom att endast till�ta en capacity p� 10 s� kommer Add att blockas n�r det finns 10 items i v�r collection.
N�r sedan en consumer tar ett item s� kan man l�gga till igen.
V�ljer man att inte s�tta en capacity s� kommer man att kunna l�gga till hur mycket man vill


	// Till�ter endast 10 items av typen int. Den kommer att blocka om vi f�rs�ker l�gga till och v�r collection �r full.
	static BlockingCollection&lt;int&gt; blockingCollection = new BlockingCollection&lt;int&gt;(10);

    //Task som l�gger till 2 tal i sekunden
    Task.Run(async () =>
    {
        for (var i = 0; i < 100; i++)
        {
            blockingCollection.Add(i);
            Console.WriteLine("Produced: " + i);
            await Task.Delay(500);
        }
    });

    //En task som endast konsumerar ett v�rde fr�n v�r collection va 15:e sekund, vilket kommer att blocka addering av items d� vi har satt capacity till 10.
    //Normalt sett s� vill man skriva och l�sa (produce/consume) s� snabbt som m�jligt, det vi g�r h�r �r bara f�r att f�rst� hur BlockingCollection&lt;T&gt; fungerar. 
    Task.Run(async () => {
        Console.WriteLine("Starting consumer");
        // Viktigt: N�r det inte finns n�gra items att l�sa kommer GetConsumingEnumerable() att blocka (v�nta) p� att ett item dyker upp.
        // Detta ska d�rf�r g�ras i en Task s� att man inte blockerar ALLT!
        foreach (var num in blockingCollection.GetConsumingEnumerable())
        {
            await Task.Delay(15000);
            Console.WriteLine("Consumed: " + num);
        }
        // Hit kommer vi inte d� blockingCollection alltid blockar p� GetConsumingEnumerable()
        Console.WriteLine("Ending consumer");
    });
            

## Custom Attribute

Vi kan genom att �rva Attribute skapa egna attribut f�r att dekorera all fr�n klasser, metoder, properties, fields, parametrar, constructor etc.

	// Exempel p� ett attribut som l�ter oss skriva en kommentar till alla typer tack vare AttributeTargets.All
    [AttributeUsage(AttributeTargets.All)]
    public class Comment : Attribute
    {
        public string Value { get; set; }
        public Comment(string v)
        {
            this.Value = v;
        }
    }

	// Anv�ndning av v�rt custom attribut
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

Vi kan med reflection hitta igen v�rt attribut och visa information om anv�ndningen av det, vilket vi g�r i n�sta avsnitt.

## Reflection

Med reflection kan vi ta reda p� information om alla Assemblies och dess inneh�ll som finns laddat i v�r AppDomain.
Vi kan s� klart ladda in nya assemblies om vi vill, men vi n�jer oss h�r med att unders�ka typen Cat i runtime.

F�r att hitta information med reflection beh�ver vi inte n�gon instans av den typ vi ska unders�ka.
Vill vi d�remot anropa metoder, k�ra get/set p� properties etc s� m�ste vi ha en instans av typen.

### Visa och �ndra objekt med reflection 
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

Genom att anv�nda metoden GetCustomAttributes kan vi hitta igen v�rt CommentAttribute
	
	var animal = typeof(Cat);
	//Find attribute with reflection
    var comment = (Comment)animal.GetCustomAttributes(typeof(Comment), false).FirstOrDefault();
    if (comment != null)
        Console.WriteLine("Comment for Cat = " + comment.Value);

### Snyggare att g�ra med extensionmetoder

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

Vi kikar p� 3 olika s�tt h�lla koll p� hur tr�dar f�r accessa kod samtidigt.

### Lock

Med lock kan vi l�sa access till koden inom v�rt lock-statement. Lock anv�nds tillsammans med en deklarerad variable.
Lock �r enkelt, men det �r viktigt att f�rst� hur det fungerar med valet av variabel vi anv�nder i lock.

Om vi tex anv�nder lock p� en static variable s� f�rhindrar detta samtidig access fr�n alla tr�dar, endast en tr�d i AppDomain kan accessa v�rt lock-statement.

	private static object locker = new object();
	lock(locker)
	{
		//lock p� static, endast en tr�d i taget kan vara h�r
	}

Om vi d�remot anv�nder en variable p� instansen s� l�ser lock f�r access inom den instans vi �r i.
	
	private object locker = new object();
	lock(locker)
	{
		//lock p� member, endast en tr�d i taget f�r denna instans kan vara h�r
	}

### SemaphoreSlim

SemaphoreSlim ger oss m�jlighet att till�ta x antal tr�dar samtidig access till ett avsnitt med kod.
Detta g�rs med metoderna Wait() och Release().

N�r man instansierar sin SemaphoreSlim kan man direkt s�tta hur m�nga som har access, samt hur m�nga som maximalt kan ha access.

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

	// metod med begr�nsad samtidig access
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

Till skillnad mot semaphoreSlim s� kan vi l�ta Semaphore vara system wide genom att ge den ett namn.
P� s� s�tt kan bara r�tt antal tr�dar accessa koden oavsett vilka processer de k�rs i, allts� inte bara AppDomain i detta fall!!!

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

	// metod med begr�nsad samtidig access SYSTEM-WIDE
	public static void DoSemaphoreStuff(string actionNr)
    {
        Console.WriteLine("Action nr {0} is waiting in semaphore land for threadid {1}", actionNr, Thread.CurrentThread.ManagedThreadId);
        semaphore.WaitOne();
        Console.WriteLine("Action nr {0} was granted access to semaphore land for threadid {1}", actionNr, Thread.CurrentThread.ManagedThreadId);
        Thread.Sleep(5000);
        Console.WriteLine("Action nr {0} work done in semaphore land, releasing threadid {1}", actionNr, Thread.CurrentThread.ManagedThreadId);
        semaphore.Release();
    }

Tips: K�r demon Semaphore genom att starta minst 2 instanser av SemaphoreDemo.exe och se att Semaphore g�ller system wide!

## PreProcessor Directives

Ni kan hitta hela listan av preprocessor directik h�r
https://msdn.microsoft.com/sv-se/library/ed8yd1ha.aspx

Direktivet nedan kommer endast att kompilera och visa en av Console.WriteLine raderna nedan.
Den andra kommer allts� att ignoreras av kompilatorn och inte ens finnas med.

	#if DEBUG
		Console.WriteLine("THIS CODE IS COMPILED & EXECUTED SINCE DEBUG IS DEFINED");
	#else
		Console.WriteLine("THIS CODE IS COMPILED & EXECUTED SINCE DEBUG IS NOT DEFINED");
	#endif

Som ett alternativ till #if #else etc kan man ocks� dekorera metoder som ska ignoreras med attributet [Conditional]

### Exempel
Metoden nedan anv�nds i Semaphore exemplet. Om vi dekorerar denna med Conditional("DEBUG") s� kommer kompilatorn att ignorera kompileringen av alla anrop till metoden om DEBUG inte �r definerat!

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

Om ni byter till Release och k�r Semaphore exemplet s� kommer inte metoden ovan att vara med i kompileringen eller anropas.