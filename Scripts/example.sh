#!/bin/sh

az appconfig feature  list -n appconfig2j46evrym7qq6 -o table
az appconfig feature set -n appconfig2j46evrym7qq6 --feature testFeature 
az appconfig kv set -n appconfig2j46evrym7qq6 --key .appconfig.featureflag/testFeature --value "{\"client_filters\": []}"
az appconfig kv set -n appconfig2j46evrym7qq6 --key .appconfig.featureflag/testFeature --value "{\"id\":\"testFeature\", \"client_filters\": [{\"name\": \"Microsoft.Targeting\", \"parameters\": {\"Audience\": {\"Users\": [], \"Groups\": [], \"DefaultRolloutPercentage\": 50}}}]}"

value=`az appconfig kv list -n appconfig2j46evrym7qq6 --key .appconfig.featureflag/testFeature | jq -c ".[].value | fromjson | .client_filters[].parameters.Audience.DefaultRolloutPercentage=25"`
az appconfig kv set -n appconfig2j46evrym7qq6 --key .appconfig.featureflag/testFeature --value $value

rollout=75
value=`az appconfig kv list -n appconfig2j46evrym7qq6 --key .appconfig.featureflag/testFeature | jq -c ".[].value | fromjson | .client_filters[].parameters.Audience.DefaultRolloutPercentage=$rollout"`
az appconfig kv set -n appconfig2j46evrym7qq6 --key .appconfig.featureflag/testFeature --value $value
