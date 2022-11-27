import { Component, EventEmitter, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Appointment } from 'app/shared/models/appointment.model';
import { ToasterService } from 'app/shared/toaster/toaster.service';
import { AddAppointmentModalComponent } from '../add-appointment-modal/add-appointment-modal.component';
import { PatientService } from '../patient.service';

@Component({
  selector: 'app-patient-appointments-page',
  templateUrl: './patient-appointments-page.component.html',
  styleUrls: ['./patient-appointments-page.component.css'],
  providers: [PatientService, ToasterService]
})
export class PatientAppointmentsPageComponent {
  constructor(public dialog: MatDialog, private patientService: PatientService, private toasterService: ToasterService) { }

  public views: {
    appointments: true,
  }

  onCreateNewAppointmentClick() {
    const dialogRef = this.dialog.open(AddAppointmentModalComponent, {
      data: {
        cabinets: [{
          id: '1',
          name: 'cab 1'
        },
        {
          id: '2',
          name: 'cab 2'
        }
        ],
        doctors: [{
          id: '1',
          name: 'doc 1'
        },
        {
          id: '2',
          name: 'doc 2'
        }
        ]
      },
      autoFocus: false
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!result)
        return;
      this.createNewAppointment(result);
    })
  }


  async createNewAppointment(appointment: Appointment) {
    const result = await this.patientService.createAppointment(appointment)
      .toPromise().catch(error => error);

    if (!result.ok) {
      this.toasterService.onError("Something went wrong !");
    }
    else {
      this.toasterService.onSuccess("Appointment created successfully !");
    }
  }
}
