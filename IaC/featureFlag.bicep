@description('Specifies the name of the App Configuration store.')
param configStoreName string = 'appconfig${uniqueString(resourceGroup().id)}'

@description('Specifies the key of the feature flag.')
param featureFlagKey string = 'Beta'

@description('Specifies the label of the feature flag. The label is optional and can be left as empty.')
param featureFlagLabel string = ''

param featureFlagDescription string = 'My Beta Flag'

param featureFlagEnabled bool = false

param featureProgressiveRollout bool = false
param featureRolloutPercentage int = 0

var featureFlagValue = {
  id: featureFlagKey
  description: featureFlagDescription
  enabled: featureFlagEnabled
}

var featureProgressive = {
  conditions: {
    client_filters: [
      {
        name: 'Microsoft.Targeting'
        parameters: {
          Audience: {
            Users: []
            Groups: []
            DefaultRolloutPercentage: featureRolloutPercentage
          }
        }
      }
    ]
  }
}

resource configStore 'Microsoft.AppConfiguration/configurationStores@2020-07-01-preview' existing = {
  name: configStoreName
}

resource configStoreName_appconfig_featureflag 'Microsoft.AppConfiguration/configurationStores/keyValues@2020-07-01-preview' = {
  parent: configStore
  name: '.appconfig.featureflag~2F${featureFlagKey}$${featureFlagLabel}'
  properties: {
    value: featureProgressiveRollout ? string(union(featureFlagValue, featureProgressive)) : string(featureFlagValue)
    contentType: 'application/vnd.microsoft.appconfig.ff+json;charset=utf-8'
  }
}

var configStoreConnectionString = listKeys(configStore.id, configStore.apiVersion).value[2].connectionString

output configStoreConnectionString string = configStoreConnectionString
output configStoreName string = configStoreName
