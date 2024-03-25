using Microsoft.AspNetCore.Components;
using System.Reflection;
using TMA_Warehouse.Shared.DTOs;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using AntDesign.Core.Extensions;
using AntDesign.TableModels;
using AntDesign;
using System;
using TMA_Warehouse.Client.Models;

namespace TMA_Warehouse.Client.Pages
{
    public class ItemBase : ComponentBase
    {
        [Inject]
        internal ItemService ItemService { get; set; }

        [Inject]
        internal NavigationManager NavigationManager { get; set; }

        [Inject]
        internal HttpClient Http { get; set; }

        internal IEnumerable<ItemFrontendModel> Items { get; set; }
        internal PropertyInfo[] Properties = typeof(ItemDTO).GetProperties();

        internal PropertyInfo CurrentSortProperty;
        internal bool ascending = false;

        internal string searchNameText = "";

        protected override async Task OnInitializedAsync()
        {
            Items = await ItemService.GetItems();
        }

        //internal void Sort(PropertyInfo propertyInfo)
        //{
        //    if (CurrentSortProperty == propertyInfo)
        //    {
        //        ascending = !ascending;
        //    }
        //    else
        //    {
        //        CurrentSortProperty = propertyInfo;
        //        ascending = false;
        //    }

        //    if (ascending)
        //    {
        //        Items = Items.OrderBy(item => CurrentSortProperty.GetValue(item)).ToArray();
        //    }
        //    else
        //    {
        //        Items = Items.OrderByDescending(item => CurrentSortProperty.GetValue(item)).ToArray();
        //    }
        //}

        internal async void UpdateItem(ItemFrontendModel item)
        {
            NavigationManager.NavigateTo($"/addItem?id={item.Id}");
        }

        internal async void RemoveItem(ItemFrontendModel item)
        {
            await Http.DeleteAsync($"Lists/Items/RemoveItem/{item.Id}"); //add delete to ItemService
            Items = await ItemService.GetItems();
            StateHasChanged();
        }

        internal async void AddItem()
        {
            NavigationManager.NavigateTo("additem");
        }

        internal async void SearchForItems(string str)
        {
            Items = await ItemService.GetItems();
            if (!string.IsNullOrEmpty(str))
            {
                Items = Items.Where(x => x.Name.ToLower().StartsWith(str.ToLower()));
            }
            StateHasChanged();
        }

        internal void definitions(string propertyName, object column)
        {
            bool setSortable = true;
            bool setFilterable = true;

            if(propertyName == "Photo")
            {
                setSortable = false;
                setFilterable = false;
            }


            column.SetValue("Sortable", setSortable);
            column.SetValue("Filterable", setFilterable);
        }
    }
}
