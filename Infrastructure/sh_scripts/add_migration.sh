cd ../
export CONN_STR='Host=localhost;Port=5432;Database=BarbecueFinance;Username=postgres;Password=root'
dotnet ef migrations add GroupType -o Data/Migrations
read -p "Press enter to continue"