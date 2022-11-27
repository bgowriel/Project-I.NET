export class Appointment {
  public id: string;
  public date: Date;
  public description: string = "";
  public status: string = "";
  public doctorId: string;
  public patientId: string;
  public officeId: string

  constructor() {
  }

}
