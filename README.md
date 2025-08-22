# CommonUtils

## Описание

`CommonUtils` — это библиотека на языке программирования C#, которая содержит утилитные классы для решения разных задач.

`StringUtils` — это статический класс, предоставляющий удобные методы для работы со строками. Он включает в себя множество полезных функций, таких как проверка на пустоту, обрезка, поиск подстрок, сравнение строк и многое другое. Этот класс является частью пространства имен `org.krista.tools`.

## Основные возможности

- **Проверка строк на пустоту и пустоту с учетом пробелов:**
  - `IsEmpty(string? s)`
  - `IsNotEmpty(string? s)`
  - `IsBlank(string? s)`
  - `IsNotBlank(string? s)`

- **Обрезка строк:**
  - `Trim(string? str)`
  - `TrimToNull(string? str)`
  - `TrimToEmpty(string? str)`

- **Обрезка начальных и конечных символов:**
  - `Strip(string? str)`
  - `StripToNull(string? str)`
  - `StripToEmpty(string? str)`
  - `StripStart(string? str, string? stripChars)`
  - `StripEnd(string? str, string? stripChars)`

- **Обработка диакритических знаков:**
  - `StripAccents(string? input)`

- **Сравнение строк:**
  - `Equals(string? str1, string? str2)`
  - `EqualsIgnoreCase(string? str1, string? str2)`
  - `Compare(string? str1, string? str2)`
  - `CompareIgnoreCase(string? str1, string? str2)`

- **Поиск подстрок:**
  - `IndexOf(string str, char searchChar)`
  - `IndexOf(string? str, string? searchStr)`
  - `IndexOf(string? str, string? searchStr, int startPos)`
  - `OrdinalIndexOf(string? str, string? searchStr, int ordinal)`
  - `IndexOfIgnoreCase(string? str, string? searchStr)`
  - `IndexOfIgnoreCase(string? str, string? searchStr, int startPos)`

- **Поиск последнего вхождения подстроки:**
  - `LastIndexOf(string? str, char searchChar)`
  - `LastIndexOf(string? str, char searchChar, int startPos)`
  - `LastIndexOf(string? str, string? searchStr)`
  - `LastOrdinalIndexOf(string? str, string? searchStr, int ordinal)`
  - `LastIndexOf(string? str, string? searchStr, int startPos)`
  - `LastIndexOfIgnoreCase(string? str, string? searchStr)`
  - `LastIndexOfIgnoreCase(string? str, string? searchStr, int startPos)`

- **Проверка на наличие символов или подстрок:**
  - `Contains(string? str, char searchChar)`
  - `Contains(string? str, string? searchStr)`
  - `ContainsIgnoreCase(string? str, string? searchStr)`
  - `ContainsWhitespace(string? str)`
  - `ContainsAny(string? str, params char[] searchChars)`
  - `ContainsAny(string? str, string? searchChars)`
  - `ContainsAny(string? str, params string?[] searchStrs)`

- **Извлечение подстрок:**
  - `Substring(string? str, int start)`
  - `Substring(string? str, int start, int end)`
  - `Left(string? str, int len)`
  - `Right(string? str, int len)`
  - `Mid(string? str, int pos, int len)`

## Примеры использования

### Проверка на пустоту

```csharp
string testString = "   ";
bool isEmpty = StringUtils.IsEmpty(testString); // false
bool isBlank = StringUtils.IsBlank(testString); // true
```

### Обрезка строк

```csharp
string testString = "  Hello, World!  ";
string trimmedString = StringUtils.Trim(testString); // "Hello, World!"
```

### Поиск подстроки

```csharp
string testString = "Hello, World!";
int index = StringUtils.IndexOf(testString, "World"); // 7
```

### Сравнение строк

```csharp
string str1 = "Hello";
string str2 = "hello";
bool areEqual = StringUtils.EqualsIgnoreCase(str1, str2); // true
```

## Зависимости

- `System`
- `System.Text`
- `System.Text.RegularExpressions`
- `System.Globalization`

## Лицензия

Этот проект распространяется под лицензией MIT. Подробности см. в файле [LICENSE](https://github.com/RomaSharper/CommonUtils/blob/main/LICENSE).

## Контакты

Для вопросов и предложений, пожалуйста, свяжитесь со мной по тг [@roma_sharper](https://t.me/roma_sharper).

---

Далее здесь будут новые разделы...
