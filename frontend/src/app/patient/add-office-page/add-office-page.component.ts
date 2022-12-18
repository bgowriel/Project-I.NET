import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { OfficeService } from 'app/services/office/office.service';

import { Office } from 'app/shared/models/office.model';
import { ToasterService } from 'app/shared/toaster/toaster.service';

@Component({
  selector: 'app-add-office-page',
  templateUrl: './add-office-page.component.html',
  styleUrls: ['./add-office-page.component.css'],
  providers: [OfficeService, ToasterService]  
})
export class AddOfficePageComponent {
  public form: FormGroup;
  public office: Office = new Office();

  public errors: any = {
    name: "",
    address: "",
    city: "",
    email: "",
    phone: "",
  }

  constructor(private router: Router,  private officeService: OfficeService, private toasterService: ToasterService) { }

  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.form = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]),
      description: new FormControl(''),
      address: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]),
      city: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]),
      email: new FormControl('', [Validators.required, Validators.email],),
      phone: new FormControl('', Validators.required),
    })
  }

  validateInput(input: string) {
    let inputName = input.charAt(0).toUpperCase() + input.slice(1);

    if (this.form.get(input)?.errors?.['required']) {
      this.errors[input] = inputName + ' is required';
      return true;
    }
    else if (this.form.get(input)?.errors?.['minlength']) {
      this.errors[input] = inputName + ' must be at least 3 characters';
      return true;
    }
    else if (this.form.get(input)?.errors?.['maxlength']) {
      this.errors[input] = inputName + ' must be at most 100 characters';
      return true;
    } else if (this.form.get(input)?.errors?.['email']) {
      this.errors[input] = 'Email is not valid';
      return true;
    }

    return false;
  }

  async onRegisterBtnClick() {
    const result = await this.officeService.registerOffice(this.office)
      .toPromise().catch(error => error);

    if (result) {
      this.toasterService.onSuccess("Office registered successfully !");
      this.form.reset();
    }
    else if (!result) {
      this.toasterService.onError("Something went wrong !");
    }

  }

}
