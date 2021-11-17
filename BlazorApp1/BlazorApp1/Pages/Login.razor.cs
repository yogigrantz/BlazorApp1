namespace BlazorApp1.Pages
{
    public partial class Login
    {

        public string UserName  {get; set;}
        public string Password { get; set; }

        public string Message { get; private set; }

        public string ISAuthenticated { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            base.OnInitializedAsync();

            string username = await authentication.IsAuthorized(httpContextAccessor, "Admin");

            if (username != null)
            {
                ISAuthenticated = $"User {username} is authenticated";
            }
        }

        public async Task LoginProcess()
        {
            Message = $"{UserName} {Password} is being processed ....";

            await Task.Delay(1000);

            bool result = await ProcessAuth(UserName, Password); 


        }

        private async Task<bool> ProcessAuth(string username, string password)
        {
            bool result = await authentication.AuthenticateAsync(UserName, Password, httpContextAccessor);
            if (result)
            {
                Message = $"{username} is authenticated";
                NavManager.NavigateTo("/fetchdata");
            }
            else
            {
                Message = $"{username} is NOT authenticated";
            }

            return result;
        }
    
    }
}
