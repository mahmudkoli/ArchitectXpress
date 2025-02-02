class Passenger {
    constructor(data) {
        this.id = data.id;
        this.firstName = data.firstName;
        this.lastName = data.lastName;
        this.email = data.email;
        this.phone = data.phone;
        this.identificationType = data.identificationType;
        this.identificationNumber = data.identificationNumber;
        this.identificationDocuments = data.identificationDocuments || [];
        this.presentAddress = data.presentAddress;
        this.permanentAddress = data.permanentAddress;
        this.status = data.status;
        this.rating = data.rating || null;
        this.referenceId = data.referenceId;
        this.createdAt = Date.now();
        this.updatedAt = Date.now();
        this.deletedAt = null;
    }
}

module.exports = Passenger;
