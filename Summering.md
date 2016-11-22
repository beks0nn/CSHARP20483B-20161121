
# ARV (Polymorfism)

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
Flera olika s�tt att skapa objekt	

##Ctor base
Vid arv anropas basklassens konstruktor f�rst				

##Object
Allt �rver object och d� object har ett par virtual method s� kan man k�ra override p� dessa i alla typer  .NET
Se ToString(), Equals(), GetHashCode()