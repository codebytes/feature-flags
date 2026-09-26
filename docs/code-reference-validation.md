# Code-reference package validation

The PR check builds the public action Docker package selected by `code-references.yml` and verifies that its scanner entrypoint exists. It does not run the scanner or upload repository data: no LaunchDarkly credentials are passed and the temporary inspection container is never started.

This proves packaging/build availability, not successful service integration. The existing LaunchDarkly workflow and its secrets are unchanged; upload authorization must be handled independently. A service-only 403 is not an application compilation failure.
