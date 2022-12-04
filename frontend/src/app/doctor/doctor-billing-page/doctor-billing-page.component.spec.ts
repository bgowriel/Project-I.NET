import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorBillingPageComponent } from './doctor-billing-page.component';

describe('DoctorBillingPageComponent', () => {
  let component: DoctorBillingPageComponent;
  let fixture: ComponentFixture<DoctorBillingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DoctorBillingPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DoctorBillingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
