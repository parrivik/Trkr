using System;
using System.Collections.Generic;
using Trkr.RestServices.DataConstruct;

using Xamarin.Forms;

namespace Trkr.UiContent
{
    public partial class Leaderboard : ContentPage
    {
        // creating struct for List of Persons from Personloader
        public IList<PersonLoader> Persons { get; private set; }


        public Leaderboard()
        {
            InitializeComponent();

            Persons = new List<PersonLoader>();
            // Hier würde die Magei stattfinden in der ich die Top 3, 5, 10 oder auch was immer mit ADO.NET (SQL statement)
            //auslese und hinzufüde


            Persons.Add(new PersonLoader
            {
                Name = "Victor",
                Surname = "Parrino",
                Location = "LANG & LONG",
                ImageUrl = "https://cdn.pixabay.com/photo/2016/10/09/18/03/smile-1726471_960_720.jpg"
            });

            Persons.Add(new PersonLoader
            {
                Name = "Peter",
                Surname = "Blattmann",
                Location = "LANG & LONG",
                ImageUrl = "https://cdn.pixabay.com/photo/2017/04/06/19/34/girl-2209147_960_720.jpg"
            });

            Persons.Add(new PersonLoader
            {
                Name = "Raffaele",
                Surname = "Bof",
                Location = "LANG & LONG",
                ImageUrl = "https://cdn.pixabay.com/photo/2013/04/07/17/57/woman-101542_960_720.jpg"
            });

            Persons.Add(new PersonLoader
            {
                Name = "Davide",
                Surname = "Ares Trinchi",
                Location = "LANG & LONG",
                ImageUrl = "https://cdn.pixabay.com/photo/2016/11/10/12/33/model-1814200_960_720.jpg"
            });


            Persons.Add(new PersonLoader
            {
                Name = "Patrick",
                Surname = "Schönen",
                Location = "LANG & LONG",
                ImageUrl = "https://cdn.pixabay.com/photo/2013/04/07/17/57/woman-101542_960_720.jpg"
            });


            Persons.Add(new PersonLoader
            {
                Name = "Salome",
                Surname = "Wenzler",
                Location = "LANG & LONG",
                ImageUrl = "https://t7.rbxcdn.com/4e6a123ada9b7bd64e4d1b0ee388c18e"
            });


            BindingContext = this;


        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            PersonLoader selectedItem = e.SelectedItem as PersonLoader;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            PersonLoader tappedItem = e.Item as PersonLoader;
        }
    }
}
