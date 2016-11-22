
# ARV (Polymorfism)

##Abstract
En abstrakt klass kan man inte skapa instanser av, men däremot ärva den och skapa instanser av implementationen

##Sealed
Sealed kan inte ärvas, det är den slutgiltiga implementationen av en klass (eller metod).

##Virtual -> Override
Virtual members kan man köra override på för att implementera custom logic/funktionalitet. 
Kan också göra sealed override så att man inte kan köra override i eventuella vidare arv

##Protected
Kan endast komma åt dessa via instansen samt de klasser som ärver instansen

##Private
Endast inom klassen

##Internal
Endast inom assemblyn!
	
##Ctor Överlagring
Flera olika sätt att skapa objekt	

##Ctor base
Vid arv anropas basklassens konstruktor först				

##Object
Allt ärver object och då object har ett par virtual method så kan man köra override på dessa i alla typer  .NET
Se ToString(), Equals(), GetHashCode()