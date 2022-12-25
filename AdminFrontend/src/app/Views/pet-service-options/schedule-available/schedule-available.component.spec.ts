import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleAvailableComponent } from './schedule-available.component';

describe('ScheduleAvailableComponent', () => {
  let component: ScheduleAvailableComponent;
  let fixture: ComponentFixture<ScheduleAvailableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ScheduleAvailableComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScheduleAvailableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
