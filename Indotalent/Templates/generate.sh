#!/bin/bash

echo "Enter the entity/controller/config name (e.g., Test):"
read name

echo "Enter the folder name for the service (e.g., Core or FeatureX):"
read service_folder

controller_dir="../ApiOData"
service_dir="../Applications/$service_folder"
config_dir="../Models/Configurations"

echo "🛠️ Generating Controller..."
dotnet new custom-controller -n "$name" -c "$name" -o "$controller_dir"

echo "🛠️ Generating Service..."
dotnet new custom-service -n "$name" -c "$name" -o "$service_dir"

echo "🛠️ Generating Configuration..."
dotnet new custom-config -n "$name" -c "$name" -o "$config_dir"

# Original default filenames
original_controller_file="$controller_dir/Controller.cs"
original_service_file="$service_dir/Service.cs"
original_config_file="$config_dir/Configuration.cs"

# Target renamed filenames
controller_file="$controller_dir/${name}Controller.cs"
service_file="$service_dir/${name}Service.cs"
config_file="$config_dir/${name}Configuration.cs"

echo "✏️ Renaming files..."

# Check and rename controller file
if [ -f "$original_controller_file" ]; then
  mv "$original_controller_file" "$controller_file"
  echo "Renamed Controller.cs to ${name}Controller.cs"
else
  echo "⚠️ Controller.cs file not found in $controller_dir"
fi

# Check and rename service file
if [ -f "$original_service_file" ]; then
  mv "$original_service_file" "$service_file"
  echo "Renamed Service.cs to ${name}Service.cs"
else
  echo "⚠️ Service.cs file not found in $service_dir"
fi

# Check and rename config file
if [ -f "$original_config_file" ]; then
  mv "$original_config_file" "$config_file"
  echo "Renamed Configuration.cs to ${name}Configuration.cs"
else
  echo "⚠️ Configuration.cs file not found in $config_dir"
fi

echo "✅ Files generated and renamed!"

read -p "Do you want to DELETE the generated files? (y/N): " delete_choice

if [[ "$delete_choice" == "y" || "$delete_choice" == "Y" ]]; then
  echo "🔄 Deleting renamed files..."

  rm -f "$controller_file" "$service_file" "$config_file"

  echo "✅ Files deleted."
else
  echo "📁 Files kept."
fi
