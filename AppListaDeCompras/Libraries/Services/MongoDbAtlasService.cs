using System.Text.Json;

using AppListaDeCompras.Models;

using Realms;
using Realms.Sync;

using User = Realms.Sync.User;

namespace AppListaDeCompras.Libraries.Services
{
    public static class MongoDbAtlasService
    {
        private static bool serviceInitialised;

        private static Realms.Sync.App app;

        private static Realm mainThreadRealm;

        public static User CurrentUser => app.CurrentUser;

        public static string DataExplorerLink;

        public static async Task Init()
        {
            if (serviceInitialised)
            {
                return;
            }

            using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("mongoDbAtlasConfig.json");
            using StreamReader reader = new(fileStream);
            var fileContent = await reader.ReadToEndAsync();

            var config = JsonSerializer.Deserialize<RealmAppConfig>(fileContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var appConfiguration = new AppConfiguration(config.AppId)
            {
                BaseUri = new Uri(config.BaseUrl)
            };

            app = Realms.Sync.App.Create(appConfiguration);

            serviceInitialised = true;

            // If you're getting this app code by cloning the repository at
            // https://github.com/mongodb/template-app-maui-todo, 
            // it does not contain the data explorer link. Download the
            // app template from the Atlas UI to view a link to your data.
            DataExplorerLink = config.DataExplorerLink;
            Console.WriteLine($"To view your data in Atlas, use this link: {DataExplorerLink}");
        }

        public static Realm GetMainThreadRealm()
        {
            return mainThreadRealm ??= GetRealm();
        }

        public static Realm GetRealm()
        {
            var config = new FlexibleSyncConfiguration(app.CurrentUser!);
            var realm = Realm.GetInstance(config);
            
            //TODO - Subscriptions
            realm.All<ListToBuy>().SubscribeAsync();
            realm.All<Product>().SubscribeAsync();
            realm.All<Models.User>().SubscribeAsync();

            return realm;
        }

        public static async Task RegisterAsync(string email, string password)
        {
            await app.EmailPasswordAuth.RegisterUserAsync(email, password);
        }

        public static async Task LoginAsync(string email, string password)
        {
            await app.LogInAsync(Credentials.EmailPassword(email, password));

            //This will populate the initial set of subscriptions the first time the realm is opened
            using var realm = GetRealm();
            await realm.Subscriptions.WaitForSynchronizationAsync();
        }
        
        public static async Task LoginAsync()
        {
            User user = CurrentUser;

            if (CurrentUser is null)
            {
                user = await app.LogInAsync(Credentials.Anonymous());
            }
            
            if (user is not null)
            {
                //This will populate the initial set of subscriptions the first time the realm is opened
                using var realm = GetRealm();
                await realm.Subscriptions.WaitForSynchronizationAsync();
            }
        }

        public static async Task LogoutAsync()
        {
            await app.CurrentUser.LogOutAsync();
            mainThreadRealm?.Dispose();
            mainThreadRealm = null;
        }

        public static async Task SetSubscription(Realm realm, SubscriptionType subType)
        {
            if (GetCurrentSubscriptionType(realm) == subType)
            {
                return;
            }

            realm.Subscriptions.Update(() =>
            {
                realm.Subscriptions.RemoveAll(true);

                //var (query, queryName) = GetQueryForSubscriptionType(realm, subType);

                //realm.Subscriptions.Add(query, new SubscriptionOptions { Name = queryName });
            });

            //There is no need to wait for synchronization if we are disconnected
            if (realm.SyncSession.ConnectionState != ConnectionState.Disconnected)
            {
                await realm.Subscriptions.WaitForSynchronizationAsync();
            }
        }

        public static SubscriptionType GetCurrentSubscriptionType(Realm realm)
        {
            var activeSubscription = realm.Subscriptions.FirstOrDefault();

            return activeSubscription.Name switch
            {
                "all" => SubscriptionType.All,
                "mine" => SubscriptionType.Mine,
                _ => throw new InvalidOperationException("Unknown subscription type")
            };
        }

        // private static (IQueryable<Item> Query, string Name) GetQueryForSubscriptionType(Realm realm, SubscriptionType subType)
        // {
        //     IQueryable<Item> query = null;
        //     string queryName = null;
        //
        //     if (subType == SubscriptionType.Mine)
        //     {
        //         query = realm.All<Item>().Where(i => i.OwnerId == CurrentUser.Id);
        //         queryName = "mine";
        //     }
        //     else if (subType == SubscriptionType.All)
        //     {
        //         query = realm.All<Item>();
        //         queryName = "all";
        //     }
        //     else
        //     {
        //         throw new ArgumentException("Unknown subscription type");
        //     }
        //
        //     return (query, queryName);
        // }
    }

    public enum SubscriptionType
    {
        Mine,
        All,
    }

    public class RealmAppConfig
    {
        public string AppId { get; set; }

        public string BaseUrl { get; set; }


        // If you're getting this app code by cloning the repository at
        // https://github.com/mongodb/template-app-maui-todo, 
        // it does not contain the data explorer link. Download the
        // app template from the Atlas UI to view a link to your data.
        public string DataExplorerLink { get; set; }
    }
}

