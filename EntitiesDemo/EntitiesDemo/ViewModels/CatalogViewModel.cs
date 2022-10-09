using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EntitiesClassLibrary;
using EntitiesClassLibrary.Models;
using EntitiesDemo.Commands;

namespace EntitiesDemo.ViewModels
{
    internal class CatalogViewModel : ViewModelBase 
    {
        /// <summary>
        /// List of Types of Entities loaded from database
        /// </summary>
        private List<EntityType> EntityTypesList { get; set; } 

        /// <summary>
        /// List of Entities loaded from database
        /// </summary>
        private List<Entity> EntitiesList { get; set; }

        private ObservableCollection<EntityType> entityTypes;
        /// <summary>
        /// This is where all types of the entities are stored
        /// </summary>
        public ObservableCollection<EntityType> EntityTypes
        {
            get { return entityTypes; }
            set { entityTypes = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// This is where all the pages with the entities are stored
        /// </summary>
        private List<ObservableCollection<Entity>> Pages { get; set; }
        
        private int currentPageNumber;
        /// <summary>
        /// Number of the current page - we need to know it to switch between pages.
        /// </summary>
        public int CurrentPageNumber
        {
            get { return currentPageNumber; }
            set 
            {
                if (value < 1)
                {
                    currentPageNumber = 1;
                }
                else if (value > Pages.Count)
                {
                    currentPageNumber = Pages.Count;
                }
                else
                {
                    currentPageNumber = value;
                }   
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Entity> currentPage;
        /// <summary>
        /// This is where entities for the current page are stored.
        /// </summary>
        public ObservableCollection<Entity> CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value;
                OnPropertyChanged();
            }
        }

        private EntityType selectedEntityType;
        /// <summary>
        /// This is where the currently selected entity type is stored
        /// </summary>
        public EntityType SelectedEntityType
        {
            get { return selectedEntityType; }
            set { selectedEntityType = value;
                Pages = PopulatePages(EntitiesList);
                CurrentPage = Pages[0];
                CurrentPageNumber = 1;
                OnPropertyChanged();
            }
        }

        private string searchBoxText;
        /// <summary>
        /// Text entered by user in the searchbox 
        /// </summary>
        public string SearchBoxText
        {
            get { return searchBoxText; }
            set { searchBoxText = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<int> itemsPerPage;
        /// <summary>
        /// Collection of integers that represent how many items are displayed on one page
        /// </summary>
        public ObservableCollection<int> ItemsPerPage
        {
            get { return itemsPerPage; }
            set { itemsPerPage = value;
                OnPropertyChanged();
            }
        }

        private int selectedItemsPerPage;
        /// <summary>
        /// Currently selected number of ItemsPerPage
        /// </summary>
        public int SelectedItemsPerPage
        {
            get { return selectedItemsPerPage; }
            set 
            { 
                selectedItemsPerPage = value;

                //this transfers user to the page that contains items from the page that was selected before SelectedItemsPerPage was changed
                if (CurrentPage.Count > 0)
                {
                    Entity lastEntity = CurrentPage[CurrentPage.Count/2];
                    Pages = PopulatePages(EntitiesList);
                    foreach (ObservableCollection<Entity> page in Pages)
                    {
                        foreach (Entity entity in page)
                        {
                            if (entity.Id == lastEntity.Id)
                            {
                                CurrentPage = page;
                                CurrentPageNumber = (Pages.FindIndex(x => x.Contains(entity)))+1;
                                break;
                            }
                        }
                        if (CurrentPage == page)
                        {
                            break;
                        }
                    } 
                }
                else
                {
                    Pages = PopulatePages(EntitiesList);
                    CurrentPage = Pages[0];
                    CurrentPageNumber = 1;
                }
                OnPropertyChanged();
            }
        }


        public DelegateCommand GoToLastPage { get; set; }
        public DelegateCommand GoToFirstPage { get; set; }
        public DelegateCommand GoToNextPage { get; set; }
        public DelegateCommand GoToPreviousPage { get; set; }
        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand GoToSpecificPageCommand { get; set; }

        public CatalogViewModel()
        {
            EntityTypesList = new List<EntityType>();
            EntitiesList = new List<Entity>();
            EntitiesList = Connector.SqlConnector.GetAllEntities();
            EntityTypesList = Connector.SqlConnector.GetAllEntityTypes();

            EntityTypes = new ObservableCollection<EntityType>(EntityTypesList);
            EntityTypes.Insert(0,new EntityType { Id = -1, TypeName = "All" });
            SelectedEntityType = EntityTypes[0];

            ItemsPerPage = new ObservableCollection<int>{ 10, 30, 50, 100, 200 };
            SelectedItemsPerPage = ItemsPerPage[0];

            if (EntitiesList.Count > 0)
            {
                CurrentPage = Pages[0];
                CurrentPageNumber = 1;

                SearchCommand = new DelegateCommand(x =>
                {
                    SelectedEntityType = EntityTypes.Where(x => x.Id == -1).FirstOrDefault();
                    List<Entity> EntitiesToShow = SearchForEntities(SearchBoxText);
                    Pages = PopulatePages(EntitiesToShow);
                    CurrentPage = Pages[0];
                    CurrentPageNumber = 1;
                });
                GoToLastPage = new DelegateCommand(x =>
                {
                    CurrentPage = Pages[Pages.Count - 1];
                    CurrentPageNumber = Pages.Count;
                });
                GoToFirstPage = new DelegateCommand(x =>
                {
                    CurrentPage = Pages[0];
                    CurrentPageNumber = 1;
                });
                GoToNextPage = new DelegateCommand(x =>
                {
                    if (CurrentPageNumber < Pages.Count)
                    {
                        CurrentPageNumber++;
                        CurrentPage = Pages[CurrentPageNumber-1]; 
                    }
                });
                GoToPreviousPage = new DelegateCommand(x =>
                {
                    if (CurrentPageNumber > 1)
                    {
                        CurrentPageNumber--;
                        CurrentPage = Pages[CurrentPageNumber-1]; 
                    }
                });
                GoToSpecificPageCommand = new DelegateCommand(x =>
                {
                    CurrentPage = Pages[CurrentPageNumber - 1];
                });
            }
        }

        private List<ObservableCollection<Entity>> PopulatePages(List<Entity> entitiesInput)
        {
            List<ObservableCollection<Entity>> pages = new List<ObservableCollection<Entity>>();
            List<Entity> entities;

            if (SelectedEntityType != null && SelectedEntityType != EntityTypes.Where(x => x.Id == -1).FirstOrDefault())
            {
                entities = entitiesInput.Where(x => x.Type.Id == SelectedEntityType.Id).ToList();
            }
            else
            {
                entities = entitiesInput;
            }

            int index = 0;
            pages.Add(new ObservableCollection<Entity>());
            for (int i = 0; i < entities.Count; i++)
            {                
                if (pages[index].Count < SelectedItemsPerPage)
                {
                    pages[index].Add(entities[i]);
                }
                else
                {
                    index++;
                    if (i!=entities.Count-1)
                    {
                        pages.Add( new ObservableCollection<Entity>());
                        pages[index].Add(entities[i]);
                    }
                }
            }

            return pages;
        }
        private List<Entity> SearchForEntities(string searchBoxText)
        {
            List<Entity> enitiesToShow = EntitiesList;
            string clippedText = searchBoxText.Trim();
            clippedText += " ";
            string word;
            for (int i = 0; i < clippedText.Length; i++)
            {
                if (clippedText[i] == ' ')
                {
                    word = clippedText.Substring(0, i);
                    enitiesToShow = enitiesToShow.Where(x => x.Name.Contains(word, StringComparison.OrdinalIgnoreCase)).ToList();

                    clippedText = clippedText.Substring(i, clippedText.Length - i);
                    if (clippedText != " ")
                    {
                        clippedText = clippedText.Trim();
                        clippedText += " "; 
                    }
                    i = 0;
                }          
            }
            return enitiesToShow;
        }
    }
}
