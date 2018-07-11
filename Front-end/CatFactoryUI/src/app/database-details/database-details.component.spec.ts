import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DatabaseDetailsComponent } from './database-details.component';

describe('DatabaseDetailsComponent', () => {
  let component: DatabaseDetailsComponent;
  let fixture: ComponentFixture<DatabaseDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DatabaseDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DatabaseDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
