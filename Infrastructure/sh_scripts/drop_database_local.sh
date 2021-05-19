cd ../
export CONN_STR='Host=localhost;Port=5432;Database=BarbecueFinance;Username=postgres;Password=root'
echo y | dotnet ef database drop
read -p "Press enter to continue"