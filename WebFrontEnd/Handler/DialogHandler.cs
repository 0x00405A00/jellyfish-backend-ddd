using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.DataTransferObject;
using WebFrontEnd.Const;
using WebFrontEnd.Service.Authentification;
using WebFrontEnd.UI.Dialog;

namespace WebFrontEnd.Handler
{
    public class DialogHandler
    {
        private readonly NavigationManager navigationManager;
        private readonly IDialogService dialogService;
        private readonly IAuthentificationService authentificationService;

        public DialogHandler(NavigationManager navigationManager, IDialogService dialogService,IAuthentificationService authentificationService)
        {
            this.navigationManager = navigationManager;
            this.dialogService = dialogService;
            this.authentificationService = authentificationService;
        }
        public async Task ShowLogoutDialog(CancellationToken cancellationToken)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters<LogoutDialog> {
            { x => x.Text, "Willlst du dich ausloggen?" },
            { x => x.SubmitButtonText, "Logout" },
            { x => x.CancelButtonText, "Abbrechen" }
        };
            var dialog = await dialogService.ShowAsync<LogoutDialog>("Logout?", options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                var response = await authentificationService.Logout(cancellationToken);
                if (response)
                {
                    navigationManager.NavigateTo(RouteConst.Routes.SignIn);
                }
            }
        }
        public enum MANAGEMENT_TYPE : int
        {
            ADD,
            EDIT
        }
        public async Task<DialogResult> ShowUserManagementDialog(MANAGEMENT_TYPE managementType, UserDTO? targetUser, List<RoleDTO> rolesList, List<UserTypeDTO> userTypesList)
        {
            string caption = null;
            switch (managementType)
            {
                case MANAGEMENT_TYPE.ADD:
                    caption = "Benutzer hinzufügen";
                    targetUser = new UserDTO();
                    break;
                case MANAGEMENT_TYPE.EDIT:
                    caption = "Benutzer editieren";
                    break;
                default:
                    break;
            }
            var user = targetUser;
            var options = new DialogOptions
            {
                FullScreen = true,
                CloseButton = true,
                CloseOnEscapeKey = true
            };
            var parameters = new DialogParameters<ManageUserDialog> {
                { x => x.User, user },
                { x => x.UserTypes, userTypesList.ToHashSet() },
                { x => x.Roles, rolesList.ToHashSet() },
                { x => x.Text, caption }
            };
            var dialog = await dialogService.ShowAsync<ManageUserDialog>(caption, parameters, options);
            return await dialog.Result;
        }
        public async Task<DialogResult> ShowUserPasswordChangeDialog(UserDTO? targetUser)
        {
            string caption = $"Changing Password for {targetUser.UserName}";
            var user = targetUser;
            var options = new DialogOptions
            {
                FullScreen = false,
                CloseButton = true,
                CloseOnEscapeKey = true
            };
            var parameters = new DialogParameters<ChangeUserPasswordDialog> {
                { x => x.User, user },
                { x => x.Text, caption }
            };
            var dialog = await dialogService.ShowAsync<ChangeUserPasswordDialog>(caption, parameters, options);
            return await dialog.Result;
        }
    }
}
