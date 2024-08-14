# ECR One Click

Aplicación de escritorio para instalación de servicios para ECR con el 
framework [MAUI](https://dotnet.microsoft.com/en-us/apps/maui).


Actualmente, la aplicación no puede ejecutarse en dispositivos Android, iOS y MacOS. Para testing y desarrollo
debe usarse un equipo de Windows.

Esto se debe a que require del servicio de Docker, por lo que se descartan dispositivos moviles por default.
Para el caso de MacOS, ocurre un error que no permite comunicarse con el daemon de Docker y no se ha encontrado
informacion sobre casos similares, por lo que se descartó el desarrollo en MacOS.

## Testing

La aplicación puede utilizar tanto contenedores como servicios (en modo Swarm). Este último __siempre__ será la
opcion principal y los contenedores son en caso de que no se puede iniciar el modo Swarm.

Hasta el momento no se han encontrado problemas al iniciar el modo Swarm - durante el desarrollo -, lo que lleva a que
se tenga que hacer un _truco_ para poder utilizar el modo de los contenedores. Para ello en la clase de `DockerService`,
en el método de `BeginSwarmMode` se indica como hacerlo.
