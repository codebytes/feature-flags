---
marp: true
theme: gaia
footer: '@Chris_L_Ayers - https://chrislayers.com'
style: |
  .columns {
    display: grid;
    grid-template-columns: repeat(2, minmax(0, 1fr));
    gap: 1rem;
  }
  .fa-th-large {
    color: blue;
  }
  .fa-users {
    color: orange;
  }
  .fa-refresh {
    color: green;
  }
  .fa-flask {
    color: brown;
  }
  .fa-ban {
    color: red;
  }
  .fa-dollar {
    color: orange;
  }
  .fa-gears {
    color: brown;
  }
  .fa-code {
    color: blue;
  }
  .fa-flag {
    color: green;
  }
  .fa-line-chart {
    color: red;
  }
  .fa-bar-chart {
    color: orange;
  }
  @import 'https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css';
---


![bg](./img/background.jpg)
# Feature Flags
## The Art of the IF and Deployment

---

![bg left](./img/portrait.jpg)

## Chris Ayers
### Senior Customer Engineer<br>Microsoft

- Twitter: @Chris\_L\_Ayers
- LinkedIn: - [chris\-l\-ayers](https://linkedin.com/in/chris-l-ayers/)
- Blog: [https://chrislayers\.com/](https://chrislayers.com/)
- GitHub: [Codebytes](https://github\.com/codebytes)

---
![bg left](./img/background.jpg)

# Agenda
- What are Feature Flags?
- Why use Feature Flags?
- Deployment vs Release
- Operationalizing Flags

---

<div class="columns">
<div>

# What Are Feature Flags?
Lets find out
- Booleans in the code
- Config values checked in

</div>
<div>

## Basics
```cs
bool featureFlag = true;
if (featureFlag) {
    // Run the following code
}
```
</div>
</div>

---

<div class="columns">
<div>

# What Are Feature Flags?
Dynamic toggling based on some information and rules

</div>
<div>

## Dynamic
```cs
bool featureFlag = isBetaUser();

if (featureFlag) {
    // Run the following code
}
```
</div>

</div>

---

# What is a Feature Flag?

Feature Flags are also known as Feature Toggles.

Feature flags can be simple configuration settings with Boolean, string or other values.

---

![bg](./img/background.jpg)
# Why Use Feature Flags?

---

<div class="columns">
<div>

# Feature Flags have different uses

</div>
<div>

#### <i class="fa fa-users"></i> Minimize Disruption to Customers

#### <i class="fa fa-refresh"></i> Progressive / Incremental Rollouts
#### <i class="fa fa-flask"></i> A/B Testing - Hypothesis Driven Development
#### <i class="fa fa-ban"></i> Kill Switch
#### <i class="fa fa-check-square-o"></i> Allow Users to Opt In

</div>
</div>

---

<div class="columns">
<div>

# Feature Flags have different uses

</div>
<div>

#### <i class="fa fa-users"></i> Block Users
#### <i class="fa fa-calendar"></i> Calendar Events
#### <i class="fa fa-newspaper-o"></i> Subscriptions
#### <i class="fa fa-sliders"></i> Advanced Users
#### <i class="fa fa-wrench"></i> Maintenance Mode
#### <i class="fa fa-th-large"></i> Code Separation
#### <i class="fa fa-power-off"></i> Sunset / Power Down

</div>
</div>

---

<div class="columns">
<div>

# Not all Flags are the same
## Short Term

</div>
<div>

- These are used to roll out new features or conduct experiments.  
- They can be found anywhere, and can be more complex. 
- They should be cleaned up after the rollout or experiment.

</div>

</div>

---
<div class="columns">
<div>

# Not all Flags are the same
## Long Term

</div>
<div>

### <i class="fa fa-dollar"></i> Licensing
### <i class="fa fa-gears"></i> Advanced features
### <i class="fa fa-code"></i> Integrations
### <i class="fa fa-flag"></i> Operational Flags
### <i class="fa fa-line-chart"></i> Load Management

</div>

</div>

---

# Deployment vs Release

<div class="columns">
<div>

## Deploy
  - Low Risk\, repeatable and routine
  - Installed on Production
  - Doesn’t mean features are in use

</div>
<div>

## Release
  - Higher Risk
  - Business decision
  - Enables access to a feature
  - Allows experimentation

</div>

</div>

---

# Limit the Blast Radius of Change

![bg right contain](./img/blast-radius.png)

---

![bg](./img/background.jpg)
# Operationalizing Flags

---

# Its all about control

#### How do you turn it on and off?
  - Per checkin?
  - Per server?
  - Per user?
  - Dynamically?


![bg right:50% contain](./img/feature-flag-management.png)

---


# Flag Targeting

<br>
<br>

<style scoped>
table {
    width: 100%;
}
</style>

|Targeting|Percentages|Triggers
|-|-|-|
|	<i class="fa fa-clock-o"></i> Time<br><i class="fa fa-map-o"></i> Region<br><i class="fa fa-user-circle-o"></i> User Details|<i class="fa fa-percent"></i> 10%/90%<br><i class="fa fa-percent"></i> 50%/50%|<i class="fa fa-line-chart"></i> Rise in failures<br><i class="fa fa-bar-chart"></i> Load

---

# Feature Flag Downsides

- Feature Flags are Technical Debt As Soon as You Add Them
- As you add flags, it can be harder to support and debug the system.
https://github.com/launchdarkly/featureflags/blob/master/2%20-%20Uses.md

---

# Flag Best Practices

- Have a naming convention for short or long term flags
- Use meaningful names with long descriptions
- Have a central location for flags, one place to look at available flags
- The development team should share flags and configurations at the end of a sprint so that the right configuration is released.
- NEVER re-purpose a feature flag


---

# SQL/JSON Models

- Be additive, never change existing fields
- If you have to remove a field, obsolete it until there is no possibility of rollback
- Separate the data model from the business logic

---

# Questions

![bg auto](./img/background.jpg)
![bg](./img/owl.png)