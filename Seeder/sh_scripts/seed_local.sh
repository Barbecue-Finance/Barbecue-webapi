cd ../
export CONN_STR='Host=localhost;Port=5432;Database=BarbecueFinance;Username=postgres;Password=root'
dotnet run -c Release
read -p "Press enter to continue"