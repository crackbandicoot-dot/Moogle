# Moogle!

![Moogle](moogle.png)

> Proyecto de Programación I.  
> Facultad de Matemática y Computación - Universidad de La Habana.  
> Cursos 2021, 2022.

Moogle! es un motor de búsqueda offline desarrollado en C# con una interfaz web en Blazor. Indexa documentos desde una carpeta configurable y permite hacer consultas con ranking por relevancia, operadores de búsqueda y soporte para archivos PDF y TXT.

## Características

- Búsqueda en archivos **.txt** y **.pdf**.
- Recorrido recursivo de la carpeta de documentos configurada.
- Ranking basado en **modelo vectorial** con similitud del coseno.
- Ponderación de consultas mediante operadores:
  - `^palabra` → la palabra debe existir.
  - `!palabra` → la palabra no debe existir.
  - `*palabra` → aumenta su peso en la consulta.
- Snippets de contexto para cada resultado.
- Configuración del directorio de datos y del número de resultados mostrados.
- Caché del último query para evitar recalcular la misma búsqueda.

## Cómo funciona

El flujo general de búsqueda es el siguiente:

1. Se carga la configuración desde `appconfig.json`.
2. Se leen los documentos del directorio configurado.
3. Se extrae y normaliza el texto de cada página o archivo.
4. Se vectoriza el corpus.
5. La consulta se compila en una expresión de operadores.
6. Se calcula un score para cada página combinando:
   - la similitud entre el vector de la query y el vector del documento;
   - la evaluación de los operadores sobre la frecuencia de palabras.
7. Se devuelven los resultados con título, snippet y páginas relevantes.

## Sintaxis de consulta

La consulta puede incluir palabras normales y operadores.

### Operadores

- `^word`: documento donde `word` exista.
- `!word`: documento donde `word` no exista.
- `*word`: da mayor relevancia a `word`.

### Ejemplos

- `algoritmos de ordenacion`
- `^pdf !imagen`
- `*programacion ^csharp`
- `!ruido *relevancia`
- `*^consulta` también es procesada como parte del compilador de operadores.

## Formatos soportados

Moogle lee documentos con estas extensiones:

- `.txt`
- `.pdf`

Los PDFs se procesan por página usando **PdfPig**.

## Modelo de ranking

El ranking combina dos ideas:

- **Similitud del coseno** entre el vector de la consulta y el vector del documento.
- **Evaluación de operadores** sobre la frecuencia de términos en la página.

En la práctica, esto favorece documentos con más términos relevantes y penaliza o filtra documentos que no cumplen los operadores de consulta.

## Estructura del proyecto

- `MoogleEngine/` → lógica de búsqueda, lectura de documentos y ranking.
- `MoogleUI/` → interfaz web.
- `MoogleController/` → controlador de escritorio para iniciar y detener la UI.
- `Shared/` → interfaces compartidas.

## Requisitos

- .NET 8.0
- Una carpeta con documentos `.txt` y/o `.pdf`

## Ejecución

Usa el script incluido o ejecuta el proyecto UI con .NET.

## Nota

Se conserva la imagen del README original por ahora.
