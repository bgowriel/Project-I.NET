import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { UserService } from 'app/services/user/user.service';
import { User } from 'app/shared/models/user.model';
import { ToasterService } from 'app/shared/toaster/toaster.service';
import { PatientService } from '../../services/patient/patient.service';
import {MatInputModule} from '@angular/material/input';

@Component({
  selector: 'app-patient-profile-page',
  templateUrl: './patient-profile-page.component.html',
  styleUrls: ['./patient-profile-page.component.css'],
  providers: [PatientService, ToasterService, UserService]
})
export class PatientProfilePageComponent implements OnInit{
  constructor( private userService: UserService, private patientService: PatientService, private toasterService: ToasterService) { }

  dataSource: any = [];

  public user: User;

  ngOnInit() {
    this.user = this.userService.getUser();
    this.getUserById(this.user.id);
  }

  getUserById(id: string) {
    const result = this.patientService.getPatientById(id).subscribe(patient=>{
      this.dataSource=[patient]
    });
  }

  updateUser(){
    const updatedUser = this.dataSource[0];
    this.patientService.updatePatient(updatedUser).subscribe(patient=>{
      this.toasterService.onSuccess("User updated successfully !");
    }, err=> {
      this.toasterService.onError("User was not updated successfully !");
    })
  }

}
