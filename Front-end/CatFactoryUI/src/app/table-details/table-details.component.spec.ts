import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TableDetailsComponent } from './table-details.component';

describe('TableDetailsComponent', () => {
  let component: TableDetailsComponent;
  let fixture: ComponentFixture<TableDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TableDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TableDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
