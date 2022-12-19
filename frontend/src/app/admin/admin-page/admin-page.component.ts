import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { OfficeService } from 'app/services/office/office.service';
import { Appointment } from 'app/shared/models/appointment.model';
import { Office } from 'app/shared/models/office.model';
import { ToasterService } from 'app/shared/toaster/toaster.service';

@Component({
  selector: 'app-admin-page',
  templateUrl: './admin-page.component.html',
  styleUrls: ['./admin-page.component.css'],
  providers: [OfficeService, ToasterService]
})
export class AdminPageComponent implements OnInit {

  displayedColumns: string[] = ['name', 'description', 'city', 'address', 'phone', 'status', 'actions'];
  dataSource: any = [];

  @ViewChild(MatTable) table: MatTable<any>;

  constructor(private officeService: OfficeService, private toasterService: ToasterService) { }

  async ngOnInit() {
    await this.getOffices();
  }

  async getOffices() {
    const result = await this.officeService.getAllOffices()
      .toPromise().catch(error => error);

    if (result) {
      //   this.toasterService.onSuccess("Offices fetched successfully !");
      console.log(result)
      result.sort(this.compare);
      this.dataSource = [...result]
    }
    else if (!result) {
      this.toasterService.onError("Something went wrong !");
    }
  }

  async onApproveBtnClick(id: string, office: Office) {
    office.status = "Approved";

    const result = await this.officeService.updateOffice(id, office).toPromise().catch(error => error);
    
    if (result) {
      this.toasterService.onSuccess("Office approved successfully !");
    }
    else if (!result.ok) {
      this.toasterService.onError("Something went wrong !");
    }
  }

  async onDeleteBtnClick(id: string) {
    const result = await this.officeService.deleteOffice(id).toPromise().catch(error => error);
    
    if (result) {
      this.toasterService.onSuccess("Office removed successfully !");
      this.dataSource.splice(this.dataSource.findIndex((office: Office) => office.id === id), 1);
      this.table.renderRows();
    }
    else if (!result.ok) {
      this.toasterService.onError("Something went wrong !");
    }
  }

  compare(a: any) {
    if (a.status == "Pending") {
      return -1;
    }
    if (a.last_nom == "Approved") {
      return 1;
    }
    return 0;
  }

}
