# ECR One Click

Aplicacion de escritorio para instalacion de servicios para ECR con el 
framework [MAUI](https://dotnet.microsoft.com/en-us/apps/maui).


Actualmente, la aplicacion no puede ejecutarse en dispositivos Android, iOS y MacOS. Para testing y desarrollo
debe usarse un equipo de Windows.

Esto se debe a que require del servicio de Docker, por lo que se descartan dispositivos moviles por default.
Para el caso de MacOS, ocurre un error que no permite comunicarse con el daemon de Docker y no se ha encontrado
informacion sobre casos similares, por lo que se descarto el desarrllo en MacOS.

## Testing

La aplicacion puede utilizar tanto contenedores como servicios (en modo Swarm). Este ultimo __siempre__ sera la
opcion principal y los contenedores son en caso de que no se puede iniciar el modo Swarm.

Hasta el momento no se han encontrado problemas al iniciar el modo Swarm - durante el desarrollo -, lo que lleva a que
se tenga que hacer un _truco_ para poder utilizar el mode de los contenedores. Para ello se tiene que visitar la clase
de `DockerService` donde se indica como hacerlo.
