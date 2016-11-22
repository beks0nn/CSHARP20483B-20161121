
# ARV (Polymorfism)
Vi använder enklast möjliga sätt (Console Application) för att skriva grundläggande CS för polymorfism

##Abstract
En abstrakt klass kan man inte skapa instanser av, men däremot ärva den och skapa instanser av implementationen

	// måste ärvas för att kunna skapa instans (abstrakt)
    public abstract class Animal
    {
        public bool Carnivore { get; set; }

        public Animal()
        {
            Console.WriteLine("CTOR - Animal");
        }
    }    

##Sealed
Sealed kan inte ärvas, det är den slutgiltiga implementationen av en klass (eller metod).

	// Sealed klass - får inte ärva pga sealed
    public sealed class Cat : Animal
    {
        public Cat()
            : base()
        {
            Console.WriteLine("CTOR - Cat");
        }
    }

	// Sealed method
	public class Person
    {
        //tostring is virtual on object, so we can override and we also make the method sealed
        public sealed override string ToString()
        {
            return string.Format("name: {0}, age: {1}", this.Name, this.Age);
        }
	}

##Virtual -> Override
Virtual members kan man köra override på för att implementera custom logic/funktionalitet. 
Kan också göra sealed override så att man inte kan köra override i eventuella vidare arv

	public class Person
    {       
        //kan köra override i ärvande klasser
        public virtual string Says()
        {
            return "Hello";
        }
    }
    public class Customer : Person
    {
        public override string Says() // lägg till sealed override för final implementation 
        {
            return "To expensive";
        }
    }


##Protected
Kan endast komma åt dessa via instansen samt de klasser som ärver instansen

	public class Person
    {
        public int Age { get; protected set; }
        public string Name { get; set; } 

		// Sätter Age via metod, kan inte skriva direkt till property pga protected
        public void SetAge(int age)
        {
            this.Age = age;
        }
	}

##Private
Endast inom klassen

##Internal
Endast inom assemblyn!

	// gömd utanför assemblyn
    internal class Lion : Animal
    {

    }
	
##Ctor Överlagring
Flera olika sätt att skapa objekt samt default parametrar

    public abstract class Vehicle
    {
        public string RegNr { get; protected set; }
        public string Type { get; protected set; }
        public virtual string DisplayInfo()
        {
            return "No info available, override the method";
        }
        public Vehicle(){}

        public Vehicle(string regnr, string type="unknown")
        {
            this.RegNr = regnr;
            this.Type = type;
        }
    }
    public class Car : Vehicle
    {
        public int Tires { get; set; }
        public Car(string regnr, int tires):base(regnr)
        {
            this.Tires = tires;
        }
        public override string DisplayInfo()
        {
            return string.Format("My car has regnr {0} and {1} tires",this.RegNr, this.Tires);
        }
    }

    public class Plane : Vehicle
    {
        public Plane(string regnr)
            : base(regnr, "PLANE")
        {

        }       
    }

##Ctor base
Vid arv anropas basklassens konstruktor först				

##Object
Allt ärver object och då object har ett par virtual method så kan man köra override på dessa i alla typer  .NET
Se ToString(), Equals(), GetHashCode()

#Collections / IEnumerable
Kollar på generiska listor och dictionaries
Bygger en enkelt WinForms Application för att lägga till och ta bort entiteter från en lista.

#LINQ-queries & Lambda-expressions
Kikar på extensionmetoder som Sum(), Average(), Where() och att dessa fungerar på allt som är IEnumerable. Se signaturen för extensionsmetoderna.
Ex Sum()
    
	public static double Average(this IEnumerable<int> source);

## LINQ vs Lambda
Vi kikade på oliak sätt att göra samma sak på en lista av integers

### Lambda-expression
    
    Func<int, bool> exp2 = x => x > 20 && x < 30;
    var result = intList.Where(exp2);

eller den kortare...

	var result = intList.Where(x => x > 20 && x < 30);

Lite förvirring kring "=>", och många tycker att queries känns enklare, men man kan också skriva med delegate om det känns enklare?

    Func<int, bool> exp1 = delegate (int x) { return x > 20 && x < 30; };
	var result = intList.Where(exp2);

eller

	var result = intList.Where(delegate (int x) { return x > 20 && x < 30; });

### Linq-query
Den mer SQL-liknande syntaxten kändes bekvämare för många, personligen föredrar jag lambda.
	
	var result = from i in intList where i > 20 && i < 30 select i;

# ExtensionMethods
En första titt på extensionmetoder. Dvs statiska klasser och metoder som har nyckelordet 'this' innan första parametern.
Extensions är bra om man vill addera funktionalitet till klasser man inte har access till, men jag använder generiska extensions oavsett då de ger funktionalitet som är bra att ha oavsett om du har access till klassen eller inte.

Signaturen för extensions är (inom en statis class)
	
	static returnType methodName(this typeToExtend parameterName);

Ofta är extensionmetoder generiska, syntaxten för detta kändes till en början konstig...
	
	static returnType methodName<T>(this T parameterName);

men jag tror syntaxten kändes enklare när vi skrev den generiska binär-serialiseraren?

#IO
Vi kikar på olika sätt att skriva/läsa till filer.

FileStream - För binär data
StreamWriter, FileStream samt de statiska metoderna på File-klassen (ReadAllText/Bytes WriteAlltext/Bytes) för enklare operationer.

#Serialization
Vi kikar på binär serialisering av objekt. I detta fall ett Dictionary<Guid,Animal> där vi vill serialisera och deserialisera vårt dictionary för att kunna läsa/skriva listan till disk via fil.
Notera att objektet (Animal) måste ha attributet [Serializable] för att detta ska fungera.

För att serialisera använde vi BinaryFormatter som tar en ström att skriva till vid serialiser och att läsa från vid deserialisering.
Vi började med MemoryStream för att se hur det fungerar, för att sedan använda FileStream för att serialisera till disk.

#Generic Binary Serilizer
Vi refaktorerade koden till en generiska extension-metoder (WriteToDisk och ReadFromDisk) som kan anändas på alla Dictionary<T,TK>.
	
	static void WriteToDisk<TK,T>(this Dictionary<TK,T> dict,  string filename = null, string path = @"c:\io\")

	static Dictionary<TK,T> ReadFromDisk<TK,T>(this Dictionary<TK, T> dict,  string filename = null, string path =  @"c:\io\")

Så nu kan vi enkelt använda dessa för att skriva/läsa
	
	var animalDb = new Dictionary<Guid,Animal>();
	//LÄgg till en dummy
    var t = new Animal { Id = Guid.NewGuid(), Name = "Tiger" };
    animalDb.Add(t.Id, t);
	// skriv till disk med vår extension
	animalDb.WriteToDisk();
	// läs från disk med vår extension
	animalDb = animalDb.ReadFromDisk();


