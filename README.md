# ItalianSyllabary
A zero dependencies .NET library to get italian words splitted in syllables. There are some known limitations, but you can even try to overcome those with the online vocabulary that Wikitionary lends us.

# Known issues
Syllables division in italian words relies on diphthongs, accents and so on.
This repo follows the guide from the following link (https://accademiadellacrusca.it/it/consulenza/divisione-in-sillabe/302).
However some words (e.g. Maria, Mario) if submitted without accents can't be splitted correctly in syllables.

For this reason you will find the Enricher that allows you to add words with accents to suggest to the library how to split correctly.
You can find this .json file into the resources directory.

# Quickstart

### Installation
Add ItalianSyllabary in the latest version of your .NET 6.0 application from the voice `"Manage NuGet packages...` of the project or simply use the following command from your CLI
```csharp
dotnet add package ItalianSyllabary --version 1.0.0
```
### Usage
```csharp
var syllabary = new ItalianSyllabary();
string[] syllables = syllabary.GetSyllables("casa"); // { "ca", "sa" }
```

# Contribute
You are free to submit pull requests. If you want, you can add words to the enrich.json file. 

A contribute could be even to reach me and telling me why are you using this library.


# License
Copyright © 2022 Carlo Francesco Pellegrino

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
