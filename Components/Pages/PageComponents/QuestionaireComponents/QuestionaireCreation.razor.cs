using AI_Project.Enums;
using AI_Project.Models.OrderModels;
using AI_Project.Services.Interfaces;
using AI_Project.ViewModels.QuestionaireComponentViewModels;
using AI_Project.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Runtime.InteropServices;

namespace AI_Project.Components.Pages.PageComponents.QuestionaireComponents
{
    public partial class QuestionaireCreation
    {
        private List<(QuestionaireComponentViewModelBase ViewModel, EComponentType ComponentType, EQuestionComponentType QuestionComponentType)> currentPageItems = new List<(QuestionaireComponentViewModelBase, EComponentType, EQuestionComponentType)>();
        private MudForm mudForm = null!;
        private int selectedPage = 1;

        [Parameter]
        [EditorRequired]
        public QuestionaireViewModel Questionaire { get; set; } = new QuestionaireViewModel()
        {
            PageList = new List<QuestionairePageViewModel>(),
            RandomGroups = new List<RandomGroupViewModel>()
        };

        private int SelectedPage
        {
            get => this.selectedPage;
            set
            {
                if (this.selectedPage == value)
                    return;
                this.selectedPage = value;
                this.RefreshCurrentItems();
                this.RefreshUI();
            }
        }

        protected override void OnInitialized()
        {
            QuestionaireViewModel questionaire = this.Questionaire;
            int num = 1;
            List<QuestionairePageViewModel> list = new List<QuestionairePageViewModel>(num);
            CollectionsMarshal.SetCount<QuestionairePageViewModel>(list, num);
            CollectionsMarshal.AsSpan<QuestionairePageViewModel>(list)[0] = new QuestionairePageViewModel()
            {
                OrderID = 1,
                Items = new List<(QuestionaireComponentViewModelBase, EComponentType, EQuestionComponentType)>()
            };
            questionaire.PageList = list;
            this.Questionaire.RandomGroups = new List<RandomGroupViewModel>();
            this.currentPageItems = this.Questionaire.PageList[0].Items;
        }

        private MudDropContainer<(QuestionaireComponentViewModelBase ViewModel, EComponentType ComponentType, EQuestionComponentType QuestionComponentType)>? MudDropContainerRef { get; set; }

        private void RefreshCurrentItems()
        {
            int? count = this.Questionaire?.PageList?.Count;
            int selectedPage = this.SelectedPage;
            if (count.GetValueOrDefault() >= selectedPage & count.HasValue && this.SelectedPage > 0)
                this.currentPageItems = this.Questionaire.PageList[this.SelectedPage - 1].Items;
            else
                this.currentPageItems = new List<(QuestionaireComponentViewModelBase viewModel, EComponentType componentType, EQuestionComponentType questionComponentType)>();
        }

        private void RefreshUI(bool forceContainerRefresh = false)
        {
            this.StateHasChanged();
            if (!forceContainerRefresh || this.MudDropContainerRef == null)
                return;
            this.MudDropContainerRef.Refresh();
        }

        private void OnItemHandled(
          (QuestionaireComponentViewModelBase ViewModel, EComponentType ComponentType, EQuestionComponentType QuestionComponentType) selectedItem)
        {
            QuestionaireComponentViewModelBase component = this.ComponentFactory.CreateComponent(selectedItem.ViewModel);
            QuestionaireComponentViewModelBase componentViewModelBase = component;
            OrderModel orderModel = new OrderModel();
            orderModel.DropZoneIdentifier = $"DestinationZone_{this.selectedPage}";
            componentViewModelBase.Order = orderModel;
            foreach (QuestionairePageViewModel page in this.Questionaire.PageList)
                page.Items.RemoveAll((Predicate<(QuestionaireComponentViewModelBase viewModelBase, EComponentType componentType, EQuestionComponentType questionComponentType)>)(i => i.viewModelBase.Id == selectedItem.ViewModel.Id));
            this.currentPageItems.Add((component, selectedItem.ComponentType, selectedItem.QuestionComponentType));
            this.RefreshUI(true);
        }

        private void HandleRandomGroupsChanged(List<RandomGroupViewModel> updatedGroups)
        {
            this.Questionaire.RandomGroups = updatedGroups;
            this.StateHasChanged();
        }

        private void AddPage()
        {
            this.Questionaire.PageList.Add(new QuestionairePageViewModel()
            {
                OrderID = this.Questionaire.PageList.Count + 1,
                Items = new List<(QuestionaireComponentViewModelBase, EComponentType, EQuestionComponentType)>()
            });
            this.SelectedPage = this.Questionaire.PageList.Count;
            this.RefreshCurrentItems();
            this.RefreshUI(true);
        }

        private void DeleteCurrentPage()
        {
            QuestionaireViewModel questionaire = this.Questionaire;
            int num1;
            if (questionaire == null)
            {
                num1 = 0;
            }
            else
            {
                int? count = questionaire.PageList?.Count;
                int num2 = 1;
                num1 = count.GetValueOrDefault() > num2 & count.HasValue ? 1 : 0;
            }
            if (num1 == 0)
                return;
            this.Questionaire.PageList.RemoveAt(this.selectedPage - 1);
            if (this.selectedPage > this.Questionaire.PageList.Count)
                this.selectedPage = this.Questionaire.PageList.Count;
            this.RefreshCurrentItems();
            this.RefreshUI(true);
        }

        private void DeletePageItem(System.Guid itemId)
        {
            QuestionairePageViewModel page = this.Questionaire.PageList[this.selectedPage - 1];
            (QuestionaireComponentViewModelBase viewModelBase, EComponentType componentType, EQuestionComponentType questionComponentType) tuple = 
                page.Items.FirstOrDefault<(QuestionaireComponentViewModelBase viewModelBase, EComponentType componentType, EQuestionComponentType questionComponentType)>
                ((Func<(QuestionaireComponentViewModelBase viewModelBase, EComponentType componentType, EQuestionComponentType questionComponentType), bool>)
                (i => i.viewModelBase.Id == itemId));
            if (tuple.Item1 == null)
                return;
            page.Items.Remove(tuple);
            this.RefreshUI(true);
        }

        private void PageItemDropped() => this.RefreshUI();

        private async Task SaveQuestionaire()
        {
            await this.mudForm.Validate();
            string publicToken;
            if (!this.mudForm.IsValid)
            {
                publicToken = (string)null;
            }
            else
            {
                this.Questionaire.PageList = this.Questionaire.PageList.Where<QuestionairePageViewModel>((Func<QuestionairePageViewModel, bool>)(page => page.Items != null && page.Items.Count > 0)).ToList<QuestionairePageViewModel>();
                for (int i = 0; i < this.Questionaire.PageList.Count; ++i)
                    this.Questionaire.PageList[i].OrderID = i + 1;
                publicToken = await this.QuestionaireService.CreateQuestionaireAsync(this.Questionaire);
                this.NavigationManager.NavigateTo("/questionairePage-reorder/" + publicToken);
                publicToken = (string)null;
            }
        }
    }
}
