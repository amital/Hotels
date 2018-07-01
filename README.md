# Template for backend services



## Using the Tempalte:

You need the template wizard to use templates. Get it here: http://tfs:8090/tfs/DefaultCollection/Payoneer/_git/services-template-wizard

The templates are located here: http://tfs:8090/tfs/DefaultCollection/Payoneer/_git/labs-template-api-service

The wizard will ask you to supply: source template, branch (use __master__ for vanilla, __flavor/state-machine__ for state-machine flavor), local directory to save results to.

Wizard configuration:

### **Remember:**
* Use swagger for debugging
* If you change the internal contracts, please run the TT transforms after building locally, in order to re-generate the DTOs.
