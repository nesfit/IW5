import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { IngredientDetailModel, IngredientListModel } from '../api/models';
import { IngredientService } from '../api/services';
import { IngredientComponent } from './ingredient.component';
import { routes } from '../app-routing.module';
import { FormsModule } from '@angular/forms';

describe('IngredientComponent', () => {
  let component: IngredientComponent;
  let fixture: ComponentFixture<IngredientComponent>;

  const ingredient1: IngredientDetailModel = { id: 'df935095-8709-4040-a2bb-b6f97cb416dc', name: 'Vejce', description: 'Popis vajec' };
  const ingredient2: IngredientDetailModel = { id: '23b3902d-7d4f-4213-9cf0-112348f56238', name: 'Cibule', description: 'Popis cibule' };
  const ingredientsArray: IngredientDetailModel[] = [ingredient1, ingredient2];
  const ingredientsObservable: Observable<Array<IngredientListModel>> = of(ingredientsArray);

  const ingredientService = jasmine.createSpyObj('IngredientService', ['ingredientGetById']);

  let getAllSpy: jasmine.Spy;
  let router: Router;

  beforeEach(async () => {
    getAllSpy = ingredientService.ingredientGetById.and.callFake((_id: any) => ingredient1);

    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule.withRoutes(routes),
        FormsModule,
      ],
      declarations: [IngredientComponent],
      providers: [
        { provide: IngredientService, useValue: ingredientService },
      ]
    })
      .compileComponents();

    router = TestBed.inject(Router);
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IngredientComponent);
    component = fixture.componentInstance;
    router.initialNavigation();
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
