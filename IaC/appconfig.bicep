@description('Specifies the name of the App Configuration store.')
param configStoreName string = 'appconfig${uniqueString(resourceGroup().id)}'

@description('Specifies the Azure location where the app configuration store should be created.')
param location string = resourceGroup().location

resource configStore 'Microsoft.AppConfiguration/configurationStores@2020-07-01-preview' = {
  name: configStoreName
  location: location
  sku: {
    name: 'standard'
  }
}

var configStoreConnectionString = listKeys(configStore.id, configStore.apiVersion).value[2].connectionString

output configStoreConnectionString string = configStoreConnectionString
output configStoreName string = configStoreName
