import { Doctor } from "./doctor.model";
import { Office } from "./office.model";

export class Appointment {
  public id: string;
  public date: Date;
  public description: string = "";
  public status: string = "";
  public doctorId: string;
  public patientId: string;
  public officeId: string
  public doctor: Doctor;
  public office: Office;

  constructor() {
  }

}
