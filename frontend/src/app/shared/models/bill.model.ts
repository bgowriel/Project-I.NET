import { Doctor } from './doctor.model';
import { User } from './user.model';

export class Bill {
  public id: string;
  public date: Date;
  public description: string;
  public amount: number;
  public doctorId: string;
  public patientId: string;
  public doctor: Doctor;
  public patient: User;

  constructor() {}
}
