import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { PatientService } from 'app/services/patient/patient.service';
import { UserService } from 'app/services/user/user.service';
import { User } from 'app/shared/models/user.model';
import { ToasterService } from 'app/shared/toaster/toaster.service';
import { DoctorService } from '../../services/doctor/doctor.service';

@Component({
  selector: 'app-doctor-profile-page',
  templateUrl: './doctor-profile-page.component.html',
  styleUrls: ['./doctor-profile-page.component.css'],
  providers: [DoctorService, ToasterService, PatientService]
})
export class DoctorProfilePageComponent implements OnInit{
  constructor( private userService: UserService, private doctorService: DoctorService, private toasterService: ToasterService, private patientService: PatientService) { }

  dataSource: any = [];

  public user: User;

  ngOnInit() {
    this.user = this.userService.getUser();
    this.getUserById(this.user.id);
  }

  getUserById(id: string) {
    const result = this.doctorService.getDoctorById(id).subscribe(doctor=>{
      this.dataSource=[doctor]
    });
  }

  updateUser(){
    const updatedUser = this.dataSource[0];
    this.doctorService.updateDoctor(updatedUser).subscribe(doctor=>{
      this.toasterService.onSuccess("User updated successfully !");
    }, err=> {
      this.toasterService.onError("User was not updated successfully !");
    })
  }
}
