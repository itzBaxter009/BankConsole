using BankConsole;

/*Client james=new Client(1,"James","james@gmail.com",4000,'M');
Employee pedro=new Employee(2,"Pedro","pedro@hotmail.com",4000,"IT");
Storage.AddUser(james);
Storage.AddUser(pedro);
*/
//EmailService.SendMail();
if(args.Length ==0)
    EmailService.SendMail();
else
    ShowMenu();

void ShowMenu(){
    Console.Clear();
    Console.WriteLine("Selecciona una opción:");
    Console.WriteLine("1 - Crear un Usuario nuevo.");
    Console.WriteLine("2 - Eliminar un Usuario existente.");
    Console.WriteLine("3 - Salir.");

    int option=0;
    do
    {
        string input=Console.ReadLine();
        if(!int.TryParse(input,out option))
            Console.WriteLine("Debes ingresar un número(1, 2 o 3).");
        else if(option>3)
            Console.WriteLine("Debes ingresar un número válido(1, 2 o 3).");                             
    }
    while(option ==0|| option>3);

    switch(option){
    case 1:
        CreateUser();
        break;
    case 2:
        DeleteUser();
        break;
    case 3:
        Environment.Exit(0);
        break;
    }
}

void CreateUser(){
    Console.Clear();
    Console.WriteLine("Ingresa la información del usuario:");

    Console.Write("ID:");
    int ID=IDValidation();//validar############

    Console.Write("Nombre:");
    string name=Console.ReadLine();

    Console.Write("Email:");
    string email=EmailValidation();    //validar################

    Console.Write("Saldo:");
    decimal balance=NaturalNumber();//validar ####################

    Console.Write("Escribe'c'si el usuario es Cliente,'e'si es Empleado:");
    char userType=UserType();//validar

    User newUser;
    if(userType.Equals('c')){
        Console.Write("Regimen Fiscal: ");
        char taxRegime=char.Parse(Console.ReadLine());
  
        newUser=new Client(ID,name,email,balance,taxRegime);
    }
    else{
        Console.Write("Departamento: ");
        string department=Console.ReadLine();
        newUser=new Employee(ID,name,email,balance,department);
    }
    Storage.AddUser(newUser);

    Console.WriteLine("Usuario creado.");
    Thread.Sleep(2000);
    ShowMenu();
}
char UserType(){
    char userType;
    bool userTypeisValid=false;
    do
    {
        userType=Console.ReadLine()[0];
        if(userType.Equals('c')||userType.Equals('e'))
            userTypeisValid=true;
        else
            Console.Write("Opcion invalida.\nEscribe'c'si el usuario es Cliente,'e'si es Empleado:");

    } while (!userTypeisValid);
    return userType;
}
void DeleteUser(){
    Console.Clear();
    Console.Write("Ingresa el ID del usuario a eliminar:");
    int ID=int.Parse(Console.ReadLine());

    string result=Storage.DeleteUser(ID);

    if(result.Equals("Success"))
        Console.Write("Usuario eliminado.");
    else    
        Console.Write($"Usuario No eliminado, No se encontro el usuario con el ID: {ID}.");
    Thread.Sleep(2000);
    ShowMenu();
    
}
int IDValidation(){
    int ID=0;
    bool IDExist=false;
    do
    {
        ID=NaturalNumber();
        if(Storage.IDValidator(ID).Equals("id already exists"))
            IDExist=true;
        else    
            IDExist=false;
        
    } while (IDExist);

    return ID;
}
string EmailValidation(){
    string Email="";
    do
    {
        Email=Console.ReadLine();
    } while (!EmailService.EmailIsValid(Email));
    return Email;
}
int NaturalNumber(){
    int numero=0;
    bool numeroValido=false;

    while(!numeroValido){
        if(int.TryParse(Console.ReadLine(), out numero) && numero>=0)
            numeroValido=true;
        else
            Console.Write("El valor ingresado es invalido, vuelve a ingresarlo\n\t:");
    }
    return numero;
}