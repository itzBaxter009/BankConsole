using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BankConsole;

public static class Storage{
    static string filePath=AppDomain.CurrentDomain.BaseDirectory + @"\user.json";
   
    public static void AddUser(User user){
        string json="",usersInFile="";
    
        if(File.Exists(filePath))
           usersInFile=File.ReadAllText(filePath);

        var listUsers=JsonConvert.DeserializeObject<List<object>>(usersInFile);

        if(listUsers == null)
         listUsers=new List<object>();
    
        listUsers.Add(user);

        JsonSerializerSettings settings = new JsonSerializerSettings{Formatting=Formatting.Indented};

        json=JsonConvert.SerializeObject(listUsers, settings);
        File.WriteAllText(filePath,json);   
    }


    public static string DeleteUser(int ID){
        string usersInFile="";
        var listUsers=new List<User>();

        if(File.Exists(filePath))
            usersInFile=File.ReadAllText(filePath);

        var listObjects=JsonConvert.DeserializeObject<List<object >>(usersInFile);
        
        if(listObjects == null)
            return "There are no user in the file";
        
        foreach(object obj in listObjects)
        {
            User newUser;
            JObject user=(JObject)obj;
            
            if(user.ContainsKey("TaxRegime"))
                newUser = user.ToObject<Client>();
            else
                newUser = user.ToObject<Employee>();
        
            listUsers.Add(newUser);
        }
        try{
            var userToDelete=listUsers.Where(user=>user.GetID()==ID).Single();
            listUsers.Remove(userToDelete);
        
                                                          
            JsonSerializerSettings settings=new JsonSerializerSettings{Formatting = Formatting.Indented};
            string json=JsonConvert.SerializeObject(listUsers,settings);

            File.WriteAllText(filePath,json);

            return"Success";
        }
        catch (System.Exception){
            
            return"Unsuccessfully";
        }
                                                             
    }



public static string IDValidator(int ID){
        string usersInFile="";
        var listUsers=new List<User>();

        if(File.Exists(filePath))
            usersInFile=File.ReadAllText(filePath);

        var listObjects=JsonConvert.DeserializeObject<List<object >>(usersInFile);
        
        if(listObjects == null)
            return "There are no user in the file";
        
        foreach(object obj in listObjects)
        {
            User newUser;
            JObject user=(JObject)obj;
            
            if(user.ContainsKey("TaxRegime"))
                newUser = user.ToObject<Client>();
            else
                newUser = user.ToObject<Employee>();
        
            listUsers.Add(newUser);
        }
        try{
            var IDUser=listUsers.Where(user=>user.GetID()==ID).Single();
            Console.WriteLine($"El ID: {ID} ya esta existe, intente con otro ID.");
            Console.Write("ID: ");
            return "id already exists";
        }
        catch (System.Exception){
            
            return"id not exists";
        }
                                                             
    }

    public static List<User> GetNewUsers()
    {
        string usersInFile="";
        var listUsers=new List<User>();

        if(File.Exists(filePath))
            usersInFile=File.ReadAllText(filePath);

        var listObjects=JsonConvert.DeserializeObject<List<object >>(usersInFile);
        
        if(listObjects == null)
            return listUsers;
        
        foreach(object obj in listObjects)
        {
            User newUser;
            JObject user=(JObject)obj;
            
            if(user.ContainsKey("TaxRegime"))
                newUser = user.ToObject<Client>();
            else
                newUser = user.ToObject<Employee>();
        
            listUsers.Add(newUser);
        }
        var newUsersList=listUsers.Where(user=>user.GetRegisterDate().Date.Equals(DateTime.Today)).ToList();

        return newUsersList;
    }                         
            


}