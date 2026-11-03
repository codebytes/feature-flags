<!-- Keep in sync with AGENTS.md at the repository root. -->

# Agent instructions: Feature Flags: The Art of the IF and Deployment

This repository holds the slides and demos for Chris Ayers' talk **Feature Flags: The Art of the IF and Deployment**. The deck is published to <https://chris-ayers.com/feature-flags/>.

## Repository layout

- `slides/`: Marp deck using the `custom-gaia` theme, images in `slides/img/`, themes in `slides/themes/`.
- `feature-flags.sln`: demo solution with `feature-management/` (Microsoft.FeatureManagement), `appconfig-sample/` (Azure App Configuration), and `launch-darkly-sample/` (LaunchDarkly).
- `IaC/`: Bicep for App Configuration and the demo web apps; `Scripts/` has helper scripts.
- `docs/code-reference-validation.md`: how the LaunchDarkly code-references workflow is validated.

## Slides

- Write the deck in Markdown with [Marp](https://marp.app/). `slides/Slides.md` is the source; `build/` is generated output and is never edited or committed.
- Keep the frontmatter at the top of the deck:

  ```yaml
  ---
  marp: true
  theme: custom-gaia
  ---
  ```

- Store themes in `slides/themes/` and images and diagram sources in `slides/img/`. Prefer theme classes and CSS variables over inline styles.
- Write speaker notes as HTML comments (`<!-- -->`).
- Render locally with the Marp CLI version the Pages workflow uses (latest):

  ```bash
  npx @marp-team/marp-cli --theme-set slides/themes --html -o build/index.html -- slides/Slides.md
  npx @marp-team/marp-cli --theme-set slides/themes --html --pdf --allow-local-files -o build/Slides.pdf -- slides/Slides.md
  ```

- `.github/workflows/marp-pages.yml` builds the HTML deck and `Slides.pdf` and publishes both to GitHub Pages. If you add slide assets outside `slides/img/` and `slides/themes/`, add them to the workflow's copy step.

## Shared skills

Use the shared skills from [codebytes/skills](https://github.com/codebytes/skills) (for Copilot CLI: `copilot plugin install https://github.com/codebytes/skills`). Don't copy their instructions or scripts into this repo.

| Task                                                                          | Skill               |
| ----------------------------------------------------------------------------- | ------------------- |
| Write or revise slides, notes, order, or layouts                              | `marp-authoring`    |
| Charts, Mermaid diagrams, or other visuals                                    | `marp-visuals`      |
| Editable `.drawio.svg` diagrams (stored in `slides/img/`)                     | `drawio-diagrams`   |
| Check overflow, clipping, and HTML/PDF parity before presenting or publishing | `marp-slide-review` |

## Build and run

```bash
dotnet build feature-flags.sln
dotnet run --project feature-management
```

## Talk-specific rules

- `appconfig-sample/global.json` pins an older SDK for that project; check it before upgrading packages.
- LaunchDarkly SDK keys and App Configuration connection strings come from configuration or secrets; never commit them.

## Readme

- Keep the readme sections in this order: Slides, Recordings, Resources, Contact.
- When a new recording of the talk is published, add it at the top of `## Recordings` as `- [Event Year](https://www.youtube.com/watch?v=VIDEO_ID)`. When the video's title differs from the talk's, add an em dash and the recorded title after the event. Keep the list newest first.
- The talk also appears on <https://chris-ayers.com/speaking/> (source: `content/speaking.md` in [codebytes/codebytes.github.io](https://github.com/codebytes/codebytes.github.io)); new events and recordings belong there too.

## General

- Keep changes scoped to the request; don't reformat unrelated slides or code.
- Never commit secrets, keys, or connection strings. Demo placeholders must be obviously fake.
