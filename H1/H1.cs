using System;
using System.Collections.Generic;

// Вывод сообщений
public interface IOutput
{
    void Write(string message);
}

// Вывод в консоль
public class ConsoleOutput : IOutput
{
    public void Write(string message)
    {
        Console.WriteLine(message);
    }
}

// Создаем класс Book
class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int Examples { get; set; }

    public Book(string title, string author, string isbn, int examples)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        Examples = examples;
    }
}

// Делаем тоже самое с классом Reader
class Reader
{
    public string Name { get; set; }
    public int Id { get; set; }

    public Reader(string name, int id)
    {
        Name = name;
        Id = id;
    }
}

class Library
{
    private List<Book> books = new List<Book>();
    private List<Reader> readers = new List<Reader>();

    private IOutput _output;

    public Library(IOutput output)
    {
        _output = output;
    }

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void RemoveBook(Book book)
    {
        books.Remove(book);
    }

    public void RegisterReader(Reader reader)
    {
        readers.Add(reader);
    }

    public void RemoveReader(Reader reader)
    {
        readers.Remove(reader);
    }

    public void LendBook(string isbn, int id)
    {
        Book book = books.Find(b => b.ISBN == isbn);
        if (book != null && book.Examples > 0)
        {
            book.Examples--;
            _output.Write($"Книга '{book.Title}' выдана читателю '{id}'");
        }
        else
        {
            _output.Write("Книги нет");
        }
    }

    public void ReturnBook(string isbn, int id)
    {
        Book book = books.Find(b => b.ISBN == isbn);
        if (book != null)
        {
            book.Examples++;
            _output.Write($"Книга '{book.Title}' возвращена читателем '{id}'");
        }
    }

    public void ListBooks()
    {
        foreach (var book in books)
        {
            _output.Write($"Название: {book.Title}, Автор: {book.Author}, ISBN: {book.ISBN}, Экземпляры: {book.Examples}");
        }
    }

    public void ListReaders()
    {
        foreach (var reader in readers)
        {
            _output.Write($"Имя: {reader.Name}, ID: {reader.Id}");
        }
    }
}

class Application
{
    static bool isConsoleOutputEnabled = true;

    static void Main(string[] args)
    {
        IOutput output;

        if (isConsoleOutputEnabled)
        {
            output = new ConsoleOutput(); // Включено - значит консоль
        }
        else
        {
            output = new NullOutput(); // Выключено - вывод в потенциальное приложение
        }

        Library library = new Library(output);

        Book book1 = new Book("1", "А1", "1111", 3);
        Book book2 = new Book("2", "А2", "2222", 5);
        library.AddBook(book1);
        library.AddBook(book2);

        Reader reader1 = new Reader("Оскар", 1);
        Reader reader2 = new Reader("Ахат", 2);
        library.RegisterReader(reader1);
        library.RegisterReader(reader2);

        library.ListBooks();
        library.ListReaders();

        library.LendBook("1111", 1);
        library.ReturnBook("1111", 1);

        library.RemoveBook(book2);
        library.ListBooks();

        library.RemoveReader(reader2);
        library.ListReaders();
    }
}

// Реализация заглушки для отключения вывода в консоль
public class NullOutput : IOutput
{
    public void Write(string message)
    {
    
    }
}