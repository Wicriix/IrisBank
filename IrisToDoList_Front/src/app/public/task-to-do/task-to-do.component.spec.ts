import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskToDoComponent } from './task-to-do.component';

describe('TaskToDoComponent', () => {
  let component: TaskToDoComponent;
  let fixture: ComponentFixture<TaskToDoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TaskToDoComponent]
    });
    fixture = TestBed.createComponent(TaskToDoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
