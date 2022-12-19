import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { OfficeService } from 'app/services/office/office.service';
import { UserService } from 'app/services/user/user.service';
import { Office } from 'app/shared/models/office.model';
import { User } from 'app/shared/models/user.model';
import { ToasterService } from 'app/shared/toaster/toaster.service';
import { DoctorService } from '../../services/doctor/doctor.service';

@Component({
  selector: 'app-doctor-join-office-page',
  templateUrl: './doctor-join-office-page.component.html',
  styleUrls: ['./doctor-join-office-page.component.css'],
  providers: [DoctorService, ToasterService, OfficeService]
})
export class DoctorJoinOfficePageComponent {

  constructor(private doctorService: DoctorService, private toasterService: ToasterService, private userService: UserService, private officeService: OfficeService) { }

  public offices: Office[] = [];
  public form: any;
  public user: User;

  async ngOnInit() {

    this.user = this.userService.getUser();

    this.form = new FormGroup({
      offices: new FormControl('', Validators.required),
    })
    await this.getAllOffices();
  }

  async getAllOffices() {
    let result = await this.officeService.getAllOffices().toPromise().catch(error => error);

    if (result) {
      result = result.filter((office: Office) => office.status == "Approved");
      this.offices = [...result];
    } else {
      this.toasterService.onError("Something went wrong");
    }
  }

  async onJoinOfficeClick() {
    const officeId = this.form.value.offices;
    console.log(officeId, this.user.id);
    const result = await this.officeService.assignDoctorToOffice(this.user.id, officeId).toPromise().catch(error => error);

    if (result) {
      this.toasterService.onSuccess("Successfully joined office");
    }
    else if (!result) {
      this.toasterService.onError("Something went wrong !");
    }
  }
}
