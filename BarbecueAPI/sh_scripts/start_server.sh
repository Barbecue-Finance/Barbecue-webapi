cd BarbecueAPI
export CONN_STR="Host=localhost;Port=5432;Database=BarbecueFinance;Username=postgres;Password=root"
export ASPNETCORE_URLS="http://+:8080\;https://+:8443"
export ASPNETCORE_ENVIRONMENT="Development"
dotnet BarbecueAPI.dll &>asp_log.txt