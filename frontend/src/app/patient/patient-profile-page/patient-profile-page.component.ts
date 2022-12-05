import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { UserService } from 'app/services/user.service';
import { User } from 'app/shared/models/user.model';
import { ToasterService } from 'app/shared/toaster/toaster.service';
import { PatientService } from '../patient.service';

@Component({
  selector: 'app-patient-profile-page',
  templateUrl: './patient-profile-page.component.html',
  styleUrls: ['./patient-profile-page.component.css'],
  providers: [PatientService, ToasterService]
})
export class PatientProfilePageComponent implements AfterViewInit{
  constructor(public dialog: MatDialog, private userService: UserService, private patientService: PatientService) { }

  displayedColumns: string[] = ['firstName', 'lastName', 'phoneNumber', 'email'];
  dataSource: any = [];

  @ViewChild(MatTable) table: MatTable<any>;

  public user: User;

  ngAfterViewInit() {
    this.user = this.userService.getUser();
    //this.asignDataToDataSource();
    this.getUserById(this.user.id);
  }

  asignDataToDataSource() {
    this.dataSource=this.user;
    this.table.renderRows();
  }

  getUserById(id: string) {
    const result = this.patientService.getPatientByPatientId(id).subscribe(patient=>{
      this.dataSource=[patient]
    });
  }
}
