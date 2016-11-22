
# ARV (Polymorfism)
Vi anv�nder enklast m�jliga s�tt (Console Application) f�r att skriva grundl�ggande CS f�r polymorfism

##Abstract
En abstrakt klass kan man inte skapa instanser av, men d�remot �rva den och skapa instanser av implementationen

##Sealed
Sealed kan inte �rvas, det �r den slutgiltiga implementationen av en klass (eller metod).

##Virtual -> Override
Virtual members kan man k�ra override p� f�r att implementera custom logic/funktionalitet. 
Kan ocks� g�ra sealed override s� att man inte kan k�ra override i eventuella vidare arv

##Protected
Kan endast komma �t dessa via instansen samt de klasser som �rver instansen

##Private
Endast inom klassen

##Internal
Endast inom assemblyn!
	
##Ctor �verlagring
Flera olika s�tt att skapa objekt samt default parametrar

##Ctor base
Vid arv anropas basklassens konstruktor f�rst				

##Object
Allt �rver object och d� object har ett par virtual method s� kan man k�ra override p� dessa i alla typer  .NET
Se ToString(), Equals(), GetHashCode()

#Collections / IEnumerable
Kollar p� generiska listor och dictionaries
Bygger en enkelt WinForms Application f�r att l�gga till och ta bort entiteter fr�n en lista.

#LINQ-queries & Lambda-expressions
Kikar p� extensionmetoder som Sum(), Average(), Where() och att dessa fungerar p� allt som �r IEnumerable. Se signaturen f�r extensionsmetoderna.
Ex Sum()
    
	public static double Average(this IEnumerable<int> source);

## LINQ vs Lambda
Vi kikade p� oliak s�tt att g�ra samma sak p� en lista av integers

### Lambda-expression
    
    Func<int, bool> exp2 = x => x > 20 && x < 30;
    var result = intList.Where(exp2);

eller den kortare...

	var result = intList.Where(x => x > 20 && x < 30);

Lite f�rvirring kring "=>", och m�nga tycker att queries k�nns enklare, men man kan ocks� skriva med delegate om det k�nns enklare?

    Func<int, bool> exp1 = delegate (int x) { return x > 20 && x < 30; };
	var result = intList.Where(exp2);

eller

	var result = intList.Where(delegate (int x) { return x > 20 && x < 30; });


### Linq-query
Den mer SQL-liknande syntaxten k�ndes bekv�mare f�r m�nga, personligen f�redrar jag lambda.
	
	var result = from i in intList where i > 20 && i < 30 select i;

# ExtensionMethods
En f�rsta titt p� extensionmetoder. Dvs statiska klasser och metoder som har nyckelordet 'this' innan f�rsta parametern.
Extensions �r bra om man vill addera funktionalitet till klasser man inte har access till, men jag anv�nder generiska extensions oavsett d� de ger funktionalitet som �r bra att ha oavsett om du har access till klassen eller inte.

Signaturen f�r extensions �r (inom en statis class)
	
	static returnType methodName(this typeToExtend parameterName);

Ofta �r extensionmetoder generiska, syntaxten f�r detta k�ndes till en b�rjan konstig...
	
	static returnType methodName<T>(this T parameterName);

men jag tror syntaxten k�ndes enklare n�r vi skrev den generiska bin�r-serialiseraren?

#IO
Vi kikar p� olika s�tt att skriva/l�sa till filer.

FileStream - F�r bin�r data
StreamWriter, FileStream samt de statiska metoderna p� File-klassen (ReadAllText/Bytes WriteAlltext/Bytes) f�r enklare operationer.

#Serialization
Vi kikar p� bin�r serialisering av objekt. I detta fall ett Dictionary<Guid,Animal> d�r vi vill serialisera och deserialisera v�rt dictionary f�r att kunna l�sa/skriva listan till disk via fil.
Notera att objektet (Animal) m�ste ha attributet [Serializable] f�r att detta ska fungera.

F�r att serialisera anv�nde vi BinaryFormatter som tar en str�m att skriva till vid serialiser och att l�sa fr�n vid deserialisering.
Vi b�rjade med MemoryStream f�r att se hur det fungerar, f�r att sedan anv�nda FileStream f�r att serialisera till disk.

#Generic Binary Serilizer
Vi refaktorerade koden till en generiska extension-metoder (WriteToDisk och ReadFromDisk) som kan an�ndas p� alla Dictionary<T,TK>.
	
	static void WriteToDisk<TK,T>(this Dictionary<TK,T> dict,  string filename = null, string path = @"c:\io\")

	static Dictionary<TK,T> ReadFromDisk<TK,T>(this Dictionary<TK, T> dict,  string filename = null, string path =  @"c:\io\")

S� nu kan vi enkelt anv�nda dessa f�r att skriva/l�sa
	
	var animalDb = new Dictionary<Guid,Animal>();
	//L�gg till en dummy
    var t = new Animal { Id = Guid.NewGuid(), Name = "Tiger" };
    animalDb.Add(t.Id, t);
	// skriv till disk med v�r extension
	animalDb.WriteToDisk();
	// l�s fr�n disk med v�r extension
	animalDb = animalDb.ReadFromDisk();


