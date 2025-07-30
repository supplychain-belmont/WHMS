#!/bin/bash
# Templates to check
required_templates=("custom-config" "custom-controller" "custom-service" "custom-dto")

# Template source (adjust this to your template path or NuGet package)
TEMPLATE_SOURCE="./" # Could be a folder or NuGet .nupkg

check_and_install_templates() {
  echo "🔍 Checking required templates..."
  for template in "${required_templates[@]}"; do
    if ! dotnet new --list | grep -q "$template"; then
      echo "❌ Template '$template' not found. Installing from $TEMPLATE_SOURCE..."
      dotnet new install "$TEMPLATE_SOURCE"
      if [ $? -ne 0 ]; then
        echo "❌ Failed to install $template. Check TEMPLATE_SOURCE path."
        exit 1
      fi
    else
      echo "✅ Template '$template' is already installed."
    fi
  done
}

check_and_install_templates

echo "Enter the entity/controller/config name (e.g., Test):"
read name

echo "Enter the folder name for the service (e.g., Core or FeatureX):"
read service_folder

echo "Verify new "

controller_dir="../ApiOData"
service_dir="../Applications/$service_folder"
config_dir="../Models/Configurations"
dto_dir="../DTOs"

echo "🛠️ Generating Controller..."
dotnet new custom-controller -n "$name" -c "$name" -o "$controller_dir"

echo "🛠️ Generating Service..."
dotnet new custom-service -n "$name" -c "$name" -o "$service_dir"

echo "🛠️ Generating Configuration..."
dotnet new custom-config -n "$name" -c "$name" -o "$config_dir"

echo "🛠️ Generating DTO..."
dotnet new custom-dto -n "$name" -c "$name" -o "$dto_dir"

# Original default filenames
original_controller_file="$controller_dir/Controller.cs"
original_service_file="$service_dir/Service.cs"
original_config_file="$config_dir/Configuration.cs"
original_dto_file="$dto_dir/Dto.cs"

# Target renamed filenames
controller_file="$controller_dir/${name}Controller.cs"
service_file="$service_dir/${name}Service.cs"
config_file="$config_dir/${name}Configuration.cs"
dto_file="$dto_dir/${name}Dto.cs"

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

# Check and rename DTO file
if [ -f "$original_dto_file" ]; then
  mv "$original_dto_file" "$dto_file"
  echo "Renamed Dto.cs to ${name}Dto.cs"
else
  echo "⚠️ Dto.cs file not found in $service_dir"
fi

echo "✅ Files generated and renamed!"

read -p "Do you want to DELETE the generated files? (y/N): " delete_choice

if [[ "$delete_choice" == "y" || "$delete_choice" == "Y" ]]; then
  echo "🔄 Deleting renamed files..."

  rm -f "$controller_file" "$service_file" "$config_file" "$dto_file"

  echo "✅ Files deleted."
else
  echo "📁 Files kept."
fi
