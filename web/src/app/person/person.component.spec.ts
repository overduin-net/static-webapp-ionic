import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { of } from 'rxjs';
import { IonicModule } from '@ionic/angular';

import { PersonComponent } from './person.component';
import { ApiService } from 'src/services/generated/api.service';

describe('ProfileComponent', () => {
  let component: PersonComponent;
  let fixture: ComponentFixture<PersonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PersonComponent ],
      imports: [
        FormsModule,
        IonicModule.forRoot()
      ],
      providers: [
        {
          provide: ApiService,
          useValue: {
            getAllPersons: () => of([]),
            createPerson: () => of({})
          }
        }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
