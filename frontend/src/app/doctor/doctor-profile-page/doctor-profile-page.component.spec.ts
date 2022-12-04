import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorProfilePageComponent } from './doctor-profile-page.component';

describe('DoctorProfilePageComponent', () => {
  let component: DoctorProfilePageComponent;
  let fixture: ComponentFixture<DoctorProfilePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DoctorProfilePageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DoctorProfilePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
