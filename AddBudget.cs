<Project Sdk="Microsoft.NET.Sdk.Web">

	<PropertyGroup>
		<TargetFramework>net8.0</TargetFramework>
		<Nullable>enable</Nullable>
		<ImplicitUsings>enable</ImplicitUsings>
	</PropertyGroup>

	<PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
		<WarningLevel>4</WarningLevel>
	</PropertyGroup>

	<PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
		<WarningLevel>4</WarningLevel>
		<DebugType>full</DebugType>
	</PropertyGroup>

	<ItemGroup>
		<PackageReference Include="Microsoft.Data.SqlClient" Version="6.0.1" />
		<PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.3" />
	</ItemGroup>

	<ItemGroup>
		<ProjectReference Include="..\BetterSpending_Business\BetterSpending_Business.csproj" />
	</ItemGroup>

	<ItemGroup>
	  <Folder Include="wwwroot\css\" />
	</ItemGroup>

</Project>
