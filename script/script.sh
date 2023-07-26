#!/bin/bash
#Definir las funciones
# Esta función recibe el nombre de la carpeta y el nombre del archivo .tex como argumentos
# Borra todos los archivos auxiliares de LaTeX que se crean durante la compilación
# Ejemplo de uso: borrar_aux carpeta ejemplo.tex

borrar_aux() {
  cd .. 
  cd $1 # Cambiar al directorio de la carpeta
  for f in *; do # Iterar sobre todos los archivos de la carpeta
    if [[ $f == *.log || $f == *.aux || $f == *.toc || $f == *.out || $f == *.nav || $f == *.snm ]]; then # Si el archivo tiene una extensión auxiliar de LaTeX
      rm $f # Borrar el archivo
    fi
  done
}

run(){
#Ir al diectorio padre
cd ..
#corer el comando make dev
make dev
}
clean(){
borrar_aux informe informe.tex
borrar_aux presentacion main.tex
cd ..
cd MoogleServer
rm -r bin
rm -r obj
cd ..
cd MoogleEngine
rm -r bin
rm -r obj
}
report(){
cd ..
cd informe
pdflatex informe.tex
}
slides(){
cd ..
cd presentacion
pdflatex main.tex
}
show_report(){
cd ..
cd informe

}
#Definir las variables con los ficheros latex
reporte="informe.tex"
presentacion="main.tex"
#Definir la herramienta de visualización por defecto
visor="evince"
#Comprobar el número de argumentos
if [ $# -eq 0 ]; then
	echo "Uso: $0 [run|clean|report|slides|show_report|show_slides]"
	exit 1
fi
#Procesar el argumento
case $1 in 
	run)
		#Ejecutar el proyecto
		echo "Ejecutando el proyecto..."
		#Aquí irá el codigo para ejecutar el proyecto
		run
		;;
	clean)
		#Eliminar los ficheros auxiliares
		echo "Limpiando los ficheros auxiliares..."
		#Aquí va el codigo para eliminar los ficheros auxiliares
		clean
		;;
	report)
		#Compilar y generar el pdf del informe
		echo "Compilando y generando el pdf del informe..."
		#Aquí va el codigo para compilar el pdf del informe
		report
		;;
	slides)
		#Compilar y generar el pdf de la presentación 
		echo "Compilando y generando el pdf de la presentación..."
		#Aquí va el codigo para compilar el pdf de la presentación
		slides
		;;
	show_report)
		#Visualizar el reporte
		#Comprobar si se ha pasado un segundo argumento con la herramienta de visualización
		if [ $# -eq 2 ]; then
			visor=$2
		fi
		#Aquí va el codigo para comprobar si está el informe pdf y si no está generarlo
		cd ..
		cd informe
		echo "En la carpeta":
		pwd
		if [ ! -f ${reporte%.*}.pdf ]; then
			echo "Generando pdf del informe..."
			pdflatex informe.tex
		fi
		#Aquí  va el codigo para abrirlo con la herramienta de visualizacion de pdf
		echo "Abriendo pdf del informe..."
		$visor ${reporte%.*}.pdf & 
		;;
	show_slides)
		#Visualizar la presentacion
		#Comprobar si se ha pasado un segundo argumento con la herramienta de visualizacion
		if [ $# -eq 2 ]; then
			visor=$2
		fi
		#Aqui va el codigo para ver si esta la presentacion pdf y si no generarlo
		cd ..
		cd presentacion
		if [ ! -f ${presentacion%.*}.pdf ]; then
			echo "Generando pdf de la presentacion..."
			pdflatex $presentacion
		fi

		#Aqui va el codigo para abrir el visor pdf
		echo "Abriendo el pdf..."
		$visor ${presentacion%.*}.pdf &
		;;
	*)
		#Argumento invalido
		echo "Argumento invalido: $1"
		echo "Uso: $0 [run|clean|report|slides|show_report|show_slides]"
		exit 2
	esac


	exit 0
		
		




