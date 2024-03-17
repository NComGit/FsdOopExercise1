using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Books
{
    public class Book
    {
        string _name;
        int _pages;
        int _publicationYear;

        public Book(string name, int pages, int publicationYear) { 
            Name = name;
            Pages = pages;
            PublicationYear = publicationYear;
        }

        public string Name { get { return _name; } set { _name = value; } }
        public int Pages { get { return _pages; } set { _pages = value; } }
        public int PublicationYear { get { return _publicationYear; } set { _publicationYear = value; } }
    }
}
