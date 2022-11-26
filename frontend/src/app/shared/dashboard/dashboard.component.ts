import { Component, EventEmitter, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddAppointmentModalComponent } from 'app/patient/add-appointment-modal/add-appointment-modal.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  @Output() onCreateNewAppointment: EventEmitter<any> = new EventEmitter();

  constructor(public dialog: MatDialog) { }

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
      this.onCreateNewAppointment.emit(result)
    })
  }
}
