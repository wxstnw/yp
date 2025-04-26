using System;
using System.Collections.Generic;
using System.Linq;
// Класс книги
class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }
    public override string ToString()
    {
        return $"\"{Title}\" by {Author}, {Year}";
    }
}
// Класс библиотеки
class Library
{
    private List<Book> books = new List<Book>();
    // Добавление книги
    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine($"Книга \"{book.Title}\" добавлена.");
    }
    // Удаление книги по названию
    public void RemoveBook(string title)
    {
        var book = books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (book != null)
        {
            books.Remove(book);
            Console.WriteLine($"Книга \"{title}\" удалена.");
        }
        else
        {
            Console.WriteLine($"Книга \"{title}\" не найдена.");
        }
    }
    // Поиск книг по автору
    public void FindBooksByAuthor(string author)
    {
        var found = books.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();

        if (found.Count == 0)
        {
            Console.WriteLine($"Книги автора \"{author}\" не найдены.");
        }
        else
        {
            Console.WriteLine($"Книги автора {author}:");
            foreach (var book in found)
            {
                Console.WriteLine(book);
            }
        }
    }
    // Отображение всех книг
    public void ShowAllBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("Библиотека пуста.");
            return;
        }
        Console.WriteLine("Список всех книг в библиотеке:");
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }
}
// Основной класс программы
class Program
{
    static void Main()
    {
        Library library = new Library();
        // Добавим несколько книг
        library.AddBook(new Book("Война и мир", "Лев Толстой", 1869));
        library.AddBook(new Book("Преступление и наказание", "Фёдор Достоевский", 1866));
        library.AddBook(new Book("Анна Каренина", "Лев Толстой", 1877));
        Console.WriteLine();
        // Поиск книг по автору
        library.FindBooksByAuthor("Лев Толстой");
        Console.WriteLine();
        // Удаление книги
        library.RemoveBook("Анна Каренина");
        Console.WriteLine();
        // Показать все книги
        library.ShowAllBooks();
    }
}
